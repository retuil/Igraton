using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMapScripts : MonoBehaviour
{
    public GameObject LeftBottomStartRoom;
    public GameObject RightUpStartRoom;
    public PlayerScript player;
    public GridScript grid;
    private GameObject playerPointer;
    private GameObject questTargetPointer;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        grid = GameObject.Find("Grid").GetComponent<GridScript>();
        LeftBottomStartRoom = GameObject.Find("LeftBottomStartRoomMap");
        RightUpStartRoom = GameObject.Find("RightUpStartRoomMap");
        playerPointer = GameObject.Find("PlayerPointer");
        questTargetPointer = GameObject.Find("QuestTargetPointer");
    }

    private void Update()
    {
        var playerPosition = (Vector2)player.transform.position;
        
        playerPointer.transform.position = GetRelativelyPosition(playerPosition);
        questTargetPointer.transform.position = GetRelativelyPosition(player.game.GetTargetPosition());
    }

    private Vector3 GetRelativelyPosition(Vector2 position)
    {
        var relativelyPosition = position - (Vector2)grid.LeftBottomStartRoom.transform.position;
        var g = grid.RightUpStartRoom.transform.position - grid.LeftBottomStartRoom.transform.position;
        var gx = g.x;
        var gy = g.y;
        var kx = relativelyPosition.x / gx;
        var ky = relativelyPosition.y / gy;
        var m = RightUpStartRoom.transform.position - LeftBottomStartRoom.transform.position;
        var mx = m.x;
        var my = m.y;
        return ((Vector2)LeftBottomStartRoom.transform.position + new Vector2(mx * kx, my * ky));
    }
}