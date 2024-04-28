using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private const float MoveSpeed = 500;

    private Rigidbody2D physic;
    private GameObject sprite;
    public GameObject gamemanager;

    public GameScript game;

    private void Start()
    {
        StartCoroutine(Tensec());
        StartCoroutine(FixedUpdateCoroutine());
        physic = GetComponent<Rigidbody2D>();
        sprite = GameObject.Find("GG");
        game = GameObject.Find("Game").GetComponent<GameScript>();
    }

    private IEnumerator Tensec()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            gamemanager.GetComponent<GameManager>().AddVision(-0.15f, -0.02f, -1);
        }
    }
    private IEnumerator FixedUpdateCoroutine()
    {
        yield return new WaitForFixedUpdate();
        game.ShowQuest0Image();
        while (true)
        {
            MovementLogic();
            RotateLogic();

            yield return new WaitForFixedUpdate();
        }
    }

    private void MovementLogic()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalInput, verticalInput, 0f) * (MoveSpeed * Time.fixedDeltaTime *
                                                                          (game.questImage.enabled ? 0f : 1f));
        physic.velocity = movement;
    }

    private void RotateLogic()
    {
        var direction = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //+ BaseAngle;
        sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation + 90));
    }
}