using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject[] elementsUI;
    [SerializeField] private GameObject MessageKill;
    [SerializeField] private GameObject tapToShot;

    [SerializeField, Space] private string headshotKillText;
    [SerializeField] private string boomKillText;
    [SerializeField] private string tripleKillText;
    [SerializeField] private string doubleKillText;

    [Header("KillMessage")]
    [SerializeField] private GameObject killMessageImage;
    [SerializeField] private TextMeshProUGUI MessageKillText;
    [SerializeField] private TextMeshProUGUI killMoneyText;

    public static UiController Instance;
    public GameObject TapToShot => tapToShot;

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

        killMessageImage.SetActive(true);
        MessageKillText.gameObject.SetActive(true);

        switch (killType)
        {
            case KillType.Headshot:
                killMoneyText.text = "+ 250";
                MessageKillText.text = headshotKillText;
                break;
            case KillType.Explosion:
                killMoneyText.text = "+ 200";
                MessageKillText.text = boomKillText;
                break;
            default:
                if (killCountInPeriod == 2)
                {
                    killMoneyText.text = "+ 200";
                    MessageKillText.text = doubleKillText;
                }
                else if (killCountInPeriod == 3)
                {
                    killMoneyText.text = "+ 300";
                    MessageKillText.text = tripleKillText;
                }
                else
                {
                    killMoneyText.text = "+ 100";
                    killMessageImage.SetActive(false);
                    MessageKillText.gameObject.SetActive(false);
                }
                break;
        }
        MessageKill.SetActive(true);

        Observable.Timer(System.TimeSpan.FromSeconds(1.5 * Time.timeScale) * Time.timeScale)
        .Subscribe(_ =>
        {
            MessageKill.SetActive(false);
        });
        /*
        Observable.Timer(System.TimeSpan.FromSeconds(10) * Time.timeScale)
       .Subscribe(_ =>
       {
           killCountInPeriod = 0;
       });
        */
    }
}
