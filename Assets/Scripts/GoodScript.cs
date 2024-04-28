using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Deth());
    }

    IEnumerator Deth()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
