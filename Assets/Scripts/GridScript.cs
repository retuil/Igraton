using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public GameObject LeftBottomStartRoom;
    public GameObject RightUpStartRoom;
    
    void Start()
    {
        LeftBottomStartRoom = GameObject.Find("LeftBottomStartRoom");
        RightUpStartRoom = GameObject.Find("RightUpStartRoom");
    }

    public void RotateTileMap()
    {
        transform.Rotate(new Vector3(0,0,90));
    }
    
}
