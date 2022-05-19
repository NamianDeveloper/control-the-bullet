using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ChangeFireModeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentModeText;
    public int CurrentID
    {
        get => currentID;
        set 
        {
            if (value >= maxCount)
            {
                currentID = 0;
            }
            else
            {
                currentID = value;
            }
        }
    }
    public static FireMode CurrentFireMode => currentFireMode;

    private int currentID;
    private static FireMode currentFireMode;
    int maxCount;

    private void Start()
    {
        maxCount = Enum.GetValues(typeof(FireMode)).Length;
    }
    public void ChangeFireMode()
    {
        CurrentID++;
        Debug.Log("Изменил Режим " + currentFireMode);
        currentFireMode = (FireMode)Enum.GetValues(typeof(FireMode)).GetValue(CurrentID);
        currentModeText.text = $"Current mode: \r\n {currentFireMode}";
    }
}
