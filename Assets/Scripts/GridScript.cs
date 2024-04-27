using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    void Start()
    {
        // RotateTileMap();
    }

    public void RotateTileMap()
    {
        transform.Rotate(new Vector3(0,0,90));
    }
    
}
