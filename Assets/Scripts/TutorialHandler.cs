using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public static TutorialHandler Instance {  get; private set; }

    [SerializeField] private List<GameObject> tutorialScreens = new List<GameObject>();
    [SerializeField] private GameObject button;


    private int activeScreen = 0;

    private void Awake()
    {
        Instance = this;
        button.SetActive(false);
    }

    public void StartTutorial()
    {
        button.SetActive(true);
        foreach (GameObject tutorialScreen in tutorialScreens)
        {
            tutorialScreen.SetActive(false);
        }
        tutorialScreens[activeScreen].SetActive(true);
    }

    public void ChangeScreen()
    {
        if(activeScreen > tutorialScreens.Count)
        {
            gameObject.SetActive(false);
            button.SetActive(false);
            return;
        }

        activeScreen = activeScreen + 1;
        foreach(GameObject tutorialScreen in tutorialScreens)
        {
            tutorialScreen.SetActive(false);
        }
        tutorialScreens[activeScreen].SetActive(true);
    }
}
