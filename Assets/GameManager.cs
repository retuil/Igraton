using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    private float visionLong = 10f, visionShort = 3f;
    private float minvisionLong = 4.5f, minvisionShort = 1.5f;
    public GameObject lightLong, lightShort;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Restore(float memvisionLong, float memvisionShort, float time)
    {
        yield return new WaitForSeconds(time);
        visionShort = memvisionShort;
        visionLong = memvisionLong;
        lightLong.GetComponent<Light2D>().pointLightOuterRadius = visionLong;
        lightShort.GetComponent<Light2D>().pointLightOuterRadius = visionShort;
    }
    public void AddVision(float addLong, float addShort, float time = -1)
    {
        if (time != -1)
        {
            StartCoroutine(Restore(visionLong,visionShort,time));
        }
        visionLong += addLong;
        visionShort += addShort;
        if (visionLong < minvisionLong)
            visionLong = minvisionLong;
        if (visionShort < minvisionShort)
            visionShort = minvisionShort;
        lightLong.GetComponent<Light2D>().pointLightOuterRadius = visionLong;
        lightShort.GetComponent<Light2D>().pointLightOuterRadius = visionShort;
    }
}
