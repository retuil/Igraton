using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private int activeQuestNumber;
    public Image questImage;

    public Sprite questImage1;
    public Sprite questImage2;
    public Sprite questImage3;
    public Sprite questImage4;
    public Sprite questImage32;
    public Sprite questImage5;

    private void Start()
    {
        activeQuestNumber = 0;
        questImage = GameObject.Find("QuestImage").GetComponent<Image>();
        questImage.enabled = false;
        StartCoroutine(FixedUpdateCoroutine());
        LoadQuestImages();
    }

    private IEnumerator FixedUpdateCoroutine()
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            if (questImage.enabled && Input.GetKey(KeyCode.Space))
                questImage.enabled = false;
            yield return new WaitForFixedUpdate();
        }
    }

    public void ProcessQuestZone1()
    {
        if (activeQuestNumber != 0) 
            return;
        activeQuestNumber = 1;
        questImage.sprite = questImage1;
        questImage.enabled = true;
    }

    public void ProcessQuestZone2()
    {
        if (activeQuestNumber != 1) 
            return;
        activeQuestNumber = 2;
        questImage.sprite = questImage2;
        questImage.enabled = true;
    }

    public void ProcessQuestZone3()
    {
        if (activeQuestNumber == 2)
        {
            activeQuestNumber = 3;
            questImage.sprite = questImage3;
            questImage.enabled = true;
        }

        if (activeQuestNumber == 4)
        {
            activeQuestNumber = 5;
            questImage.sprite = questImage32;
            questImage.enabled = true;
        }
    }

    public void ProcessQuestZone4()
    {
        if (activeQuestNumber != 3) 
            return;
        activeQuestNumber = 4;
        questImage.sprite = questImage4;
        questImage.enabled = true;
    }

    public void ProcessQuestZone5()
    {
        if (activeQuestNumber != 5) 
            return;
        activeQuestNumber = 6;
        questImage.sprite = questImage5;
        questImage.enabled = true;
    }

    private void LoadQuestImages()
    {
        questImage1 = Resources.Load<Sprite>("QuestImages/Quest1");
        questImage2 = Resources.Load<Sprite>("QuestImages/Quest2");
        questImage3 = Resources.Load<Sprite>("QuestImages/Quest3");
        questImage4 = Resources.Load<Sprite>("QuestImages/Quest4");
        questImage32 = Resources.Load<Sprite>("QuestImages/Quest3.2");
        questImage5 = Resources.Load<Sprite>("QuestImages/Quest5");
    }
}