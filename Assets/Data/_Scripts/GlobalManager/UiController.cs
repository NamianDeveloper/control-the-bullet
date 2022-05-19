using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject[] elementsUI;

    public static UiController Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
    public void ShowUiElements(bool showMode = true)
    {
        foreach (GameObject elementUI in elementsUI)
        {
            elementUI.SetActive(showMode);
        }
    }
}
