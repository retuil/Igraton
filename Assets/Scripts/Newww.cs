using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newww : MonoBehaviour
{
    [SerializeField] public static GameObject master;
    [SerializeField] public static GameObject map;
    
    public static void Function()
    {
        map.SetActive(false);
        master.GetComponent<GameManager>().AddVision(-100, -100);
    }
}
