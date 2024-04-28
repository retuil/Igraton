using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject boom;
    public void De()
    {
        Debug.Log(this.GetComponentInChildren<AudioSource>());
        this.GetComponentInChildren<AudioSource>().Play();
        GameObject b =  Instantiate(boom);
        b.transform.position = this.gameObject.transform.position;
        StartCoroutine(A());
    }

    IEnumerator A()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
