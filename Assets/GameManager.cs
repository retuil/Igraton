using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject loseScreen;
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

    public void ResumeTime()
    {
        Time.timeScale = 1;
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

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject game;
    private int current_location = 0;
    public void Death()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        player.gameObject.transform.position = spawnPoints[game.gameObject.GetComponent<GameScript>().activeQuestNumber].transform.position;
        player.gameObject.GetComponent<HealthPoints>().Prepare();
    }
    
}
