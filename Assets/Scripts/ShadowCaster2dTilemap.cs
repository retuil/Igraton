using System.Linq;
using System.Numerics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;


#if UNITY_EDITOR

[RequireComponent(typeof(CompositeCollider2D))]
public class ShadowCaster2DCreator : MonoBehaviour
{
	[SerializeField]
	private bool selfShadows = true;

	private CompositeCollider2D tilemapCollider;
	private Tilemap tilemap;
	static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
	static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
	static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
	static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)
									.Assembly
									.GetType("UnityEngine.Rendering.Universal.ShadowUtility")
									.GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);

	private Vector3[] FindPath(bool up, bool down, bool left, bool right, Vector3 coords)
	{
		const float padding = 0.25f;
		Vector3[] ans = new Vector3[12];
		if (down)
		{
			ans[0] = coords + new Vector3(1 - padding,padding,0);
			ans[1] = coords + new Vector3(1 - padding,0,0);
			ans[2] = coords + new Vector3(padding,0,0);
		}
		else
		{
			ans[0] = coords + new Vector3(1 - padding,padding,0);
			ans[1] = coords + new Vector3(1 - padding,padding,0);
			ans[2] = coords + new Vector3(padding,padding,0);
		}
		if (left)
		{
			ans[3] = coords + new Vector3(padding,padding,0);
			ans[4] = coords + new Vector3(0,padding,0);
			ans[5] = coords + new Vector3(0,1 - padding,0);
		}
		else
		{
			ans[3] = coords + new Vector3(padding,padding,0);
			ans[4] = coords + new Vector3(padding,padding,0);
			ans[5] = coords + new Vector3(padding,1 - padding,0);
		}
		if (up)
		{
			ans[6] = coords + new Vector3(padding,1 - padding,0);
			ans[7] = coords + new Vector3(padding,1,0);
			ans[8] = coords + new Vector3(1 - padding,1,0);
		}
		else
		{
			ans[6] = coords + new Vector3(padding,1 - padding,0);
			ans[7] = coords + new Vector3(padding,1 - padding,0);
			ans[8] = coords + new Vector3(1 - padding,1 - padding,0);
		}
		if (right)
		{
			ans[9] = coords + new Vector3(1 - padding,1 - padding,0);
			ans[10] = coords + new Vector3(1, 1 - padding,0);
			ans[11] = coords + new Vector3(1, padding, 0);
		}
		else
		{
			ans[9] = coords + new Vector3(1 - padding,1 - padding,0);
			ans[10] = coords + new Vector3(1 - padding, 1 - padding,0);
			ans[11] = coords + new Vector3(1 - padding, padding, 0);
		}

		return ans;
	}
	public void Create()
	{
		DestroyOldShadowCasters();
		tilemap = GetComponent<Tilemap>();
		var bounds = tilemap.cellBounds;
		for (var x = bounds.xMin; x < bounds.xMax; x++)
		for (var y = bounds.yMin; y < bounds.yMax; y++)
		{
			var tilePositionInt = new Vector3Int(x, y, 0);
			var tilePosition = new Vector3(tilePositionInt.x,tilePositionInt.y,tilePositionInt.z);
			var tile = tilemap.GetTile(tilePositionInt);
			if (tile is null)
			{
				continue;
			}

			tilePositionInt.y += 1;
			var t_up = tilemap.GetTile(tilePositionInt) is not null;
			tilePositionInt.y -= 2;
			var t_down = tilemap.GetTile(tilePositionInt) is not null;
			tilePositionInt.y += 1;
			tilePositionInt.x -= 1;
			var t_left = tilemap.GetTile(tilePositionInt) is not null;
			tilePositionInt.x += 2;
			var t_right = tilemap.GetTile(tilePositionInt) is not null;
			GameObject shadowCaster = new GameObject("shadow_caster_" + x+ "_" + y);
			shadowCaster.transform.parent = gameObject.transform;
			ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
			shadowCasterComponent.selfShadows = this.selfShadows;

			Vector3[] testPath = FindPath(t_up, t_down, t_left, t_right, tilePosition);
			shapePathField.SetValue(shadowCasterComponent, testPath);
			shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));
			meshField.SetValue(shadowCasterComponent, new Mesh());
			generateShadowMeshMethod.Invoke(shadowCasterComponent,
				new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent) });
		}
		
	}
	public void DestroyOldShadowCasters()
	{

		var tempList = transform.Cast<Transform>().ToList();
		foreach (var child in tempList)
		{
			DestroyImmediate(child.gameObject);
		}
	}
}

[CustomEditor(typeof(ShadowCaster2DCreator))]
public class ShadowCaster2DTileMapEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Create"))
		{
			var creator = (ShadowCaster2DCreator)target;
			creator.Create();
		}

		if (GUILayout.Button("Remove Shadows"))
		{
			var creator = (ShadowCaster2DCreator)target;
			creator.DestroyOldShadowCasters();
		}
		EditorGUILayout.EndHorizontal();
	}

}

#endif