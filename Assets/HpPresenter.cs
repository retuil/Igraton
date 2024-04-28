using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HpPresenter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = ""+player.GetComponent<HealthPoints>().health;
    }

    void Update()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = ""+player.GetComponent<HealthPoints>().health;

    }
}
