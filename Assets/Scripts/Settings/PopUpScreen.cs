using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> ObjectList;

    private bool isVisible;
    private void Start()
    {
        isVisible = false;
        foreach (GameObject obj in ObjectList)
        {
            obj.SetActive(false);
        }
    }

    public void ToggleScreen()
    {
        isVisible = !isVisible;
        SoundManager.Instance.PlayClickSound();

        if (isVisible)
        {
            foreach (GameObject obj in ObjectList)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in ObjectList)
            {
                obj.SetActive(false);
            }
        }
    }

}
