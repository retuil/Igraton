using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    public int _max_health = 3;
    public int health = 3;
    private bool _неуязвимый = false;
    public void Prepare()
    {
        health = _max_health;
        _неуязвимый = false;
    }

    private void НеНеуязвимый()
    {
        _неуязвимый = false;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_неуязвимый)
        {
            return;
        }

        
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
            _неуязвимый = true;
            Invoke("НеНеуязвимый",1);
        }
        if (other.gameObject.CompareTag("Attack"))
        {
            health--;
            _неуязвимый = true;
            Invoke("НеНеуязвимый",1);
            other.GetComponent<BadScript>().De();
        }
        if (health==0)
        {
            gameManager.GetComponent<GameManager>().Death();
        }
    }
}