using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private int _max_health = 3;
    public int health = 42;
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
    

    private void OnCollisionEnter(Collision other)
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
            Destroy(other.gameObject);
        }
        if (health==0)
        {
            gameManager.GetComponent<GameManager>().Death();
        }
    }
}