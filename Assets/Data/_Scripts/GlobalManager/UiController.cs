using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject[] elementsUI;
    [SerializeField] private GameObject MessageKill;
    [SerializeField] private TextMeshProUGUI MessageKillText;

    public static UiController Instance;

    private int killCountInPeriod;
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
    public void ShowMessageKill(KillType killType)
    {
        killCountInPeriod++;

       
        switch (killType)
        {
            case KillType.Headshot:
                MessageKillText.text = "������!";
                break;
            case KillType.Explosion:
                MessageKillText.text = "����!";
                break;
            default:
                if (killCountInPeriod == 2)
                {
                    MessageKillText.text = "��������!";
                }
                else if (killCountInPeriod == 3)
                {
                    MessageKillText.text = "���������!";
                }
                else
                {
                    return;
                }
                break;
        }

        MessageKill.SetActive(true);

        Observable.Timer(System.TimeSpan.FromSeconds(3) * Time.timeScale)
        .Subscribe(_ =>
        {
            MessageKill.SetActive(false);
        });
        Observable.Timer(System.TimeSpan.FromSeconds(10) * Time.timeScale)
       .Subscribe(_ =>
       {
           killCountInPeriod = 0;
       });
    }
}
