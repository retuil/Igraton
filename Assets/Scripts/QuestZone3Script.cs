using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone3Script : MonoBehaviour
{
    private GameScript game;

    private void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area 3!");
            game.ProcessQuestZone3();
        }
    }
}