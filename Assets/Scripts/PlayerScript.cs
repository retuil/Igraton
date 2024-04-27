using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private const float moveSpeed = 400;

    private Rigidbody2D physic;

    private void Start()
    {
        StartCoroutine(FixedUpdateCoroutine());
        physic = GetComponent<Rigidbody2D>();
    }

    private IEnumerator FixedUpdateCoroutine()
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            MovementLogic();
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