using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private const float moveSpeed = 10;

    private void Start()
    {
        StartCoroutine(FixedUpdateCoroutine());
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
        transform.Translate(movement);
    }
}