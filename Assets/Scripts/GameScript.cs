using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public int activeQuestNumber;
    public Image questImage;
    public GridScript grid;

    public Sprite questImage1;
    public Sprite questImage2;
    public Sprite questImage3;
    public Sprite questImage4;
    public Sprite questImage32;
    public Sprite questImage5;

    public Tilemap Tunnel1;
    public Tilemap Tunnel2;
    public Tilemap Tunnel3;
    public Tilemap Tunnel4;
    public Tilemap Tunnel5;
    public Tilemap Tunnel6;

    [SerializeField] private GameObject winScreen;

    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridScript>();

        activeQuestNumber = 0;
        questImage = GameObject.Find("QuestImage").GetComponent<Image>();
        questImage.enabled = false;
        StartCoroutine(FixedUpdateCoroutine());
        LoadQuestImages();
        InitTileMap();
    }

    private IEnumerator FixedUpdateCoroutine()
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            if (questImage.enabled && Input.GetKey(KeyCode.Space))
                questImage.enabled = false;
            yield return new WaitForFixedUpdate();
        }
    }

    public void ProcessQuestZone1()
    {
        if (activeQuestNumber != 0)
            return;
        activeQuestNumber = 1;
        Tunnel1.gameObject.SetActive(false);
        Tunnel2.gameObject.SetActive(true);
        questImage.sprite = questImage1;
        questImage.enabled = true;
    }

    public void ProcessQuestZone2()
    {
        if (activeQuestNumber != 1)
            return;
        activeQuestNumber = 2;
        Tunnel2.gameObject.SetActive(false);
        Tunnel3.gameObject.SetActive(true);
        questImage.sprite = questImage2;
        questImage.enabled = true;
    }

    public void ProcessQuestZone3()
    {
        if (activeQuestNumber == 2)
        {
            activeQuestNumber = 3;
            Tunnel3.gameObject.SetActive(false);
            Tunnel4.gameObject.SetActive(true);
            questImage.sprite = questImage3;
            questImage.enabled = true;
        }

        if (activeQuestNumber == 4)
        {
            activeQuestNumber = 5;
            Tunnel5.gameObject.SetActive(false);
            Tunnel6.gameObject.SetActive(true);
            questImage.sprite = questImage32;
            questImage.enabled = true;
        }
    }

    public void ProcessQuestZone4()
    {
        if (activeQuestNumber != 3)
            return;
        activeQuestNumber = 4;
        Tunnel4.gameObject.SetActive(false);
        Tunnel5.gameObject.SetActive(true);
        questImage.sprite = questImage4;
        questImage.enabled = true;
    }

    public IEnumerator asd()
    {
        yield return new WaitForSeconds(5);
        winScreen.gameObject.SetActive(true);
        
    }
    public void ProcessQuestZone5()
    {
        if (activeQuestNumber != 5)
            return;
        activeQuestNumber = 6;
        Tunnel6.gameObject.SetActive(false);
        questImage.sprite = questImage5;
        questImage.enabled = true;
        StartCoroutine(asd());
    }

    private void InitTileMap()
    {
        var tileMaps = grid.GetComponentsInChildren<Tilemap>().Where(t => t.name.Contains("Tunnel")).OrderBy(t => t.name).ToArray();
        Debug.Log(string.Join(" ", tileMaps.Select(r => r.ToString())));
        Tunnel1 = tileMaps[0];
        Tunnel2 = tileMaps[1];
        Tunnel3 = tileMaps[2];
        Tunnel4 = tileMaps[3];
        Tunnel5 = tileMaps[4];
        Tunnel6 = tileMaps[5];
        foreach (var tM in tileMaps)
            tM.gameObject.SetActive(false);
        Tunnel1.gameObject.SetActive(true);

        // Tunnel1.enabled = true;
    }

    private void LoadQuestImages()
    {
        questImage1 = Resources.Load<Sprite>("QuestImages/Quest1");
        questImage2 = Resources.Load<Sprite>("QuestImages/Quest2");
        questImage3 = Resources.Load<Sprite>("QuestImages/Quest3");
        questImage4 = Resources.Load<Sprite>("QuestImages/Quest4");
        questImage32 = Resources.Load<Sprite>("QuestImages/Quest3.2");
        questImage5 = Resources.Load<Sprite>("QuestImages/Quest5");
    }

    public Vector2 GetTargetPosition()
    {
        return activeQuestNumber switch
        {
            0 => GameObject.Find("QuestZone1").transform.position,
            1 => GameObject.Find("QuestZone2").transform.position,
            2 => GameObject.Find("QuestZone3").transform.position,
            3 => GameObject.Find("QuestZone4").transform.position,
            4 => GameObject.Find("QuestZone3").transform.position,
            5 => GameObject.Find("QuestZone5").transform.position,
            _ => Vector2.zero
        };
    }
}