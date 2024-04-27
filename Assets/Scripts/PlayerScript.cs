using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private const float moveSpeed = 400;

    private Rigidbody2D physic;
    private GameObject sprite;

    private void Start()
    {
        StartCoroutine(FixedUpdateCoroutine());
        physic = GetComponent<Rigidbody2D>();
        sprite = GameObject.Find("GG");
    }

    private IEnumerator FixedUpdateCoroutine()
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            MovementLogic();

            var direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //+ BaseAngle;
            sprite.transform.rotation = Quaternion.Euler(new Vector3(0,0,rotation+90));
            yield return new WaitForFixedUpdate();
        }
    }

    private void MovementLogic()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalInput, verticalInput, 0f) * (moveSpeed * Time.fixedDeltaTime);
        physic.velocity = movement;

        // transform.Translate(movement);
    }
}