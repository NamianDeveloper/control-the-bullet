using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject moneyPrefab;

    public static MoneyController Instance;

    [SerializeField] private Transform[] path;

    private int money;
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
        if (PlayerPrefs.HasKey("money")) money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString();
    }

    public void AddMoney(KillType killType)
    {
        int AddMoney = 0;
        switch (killType)
        {
            case KillType.Body:
                AddMoney = 100;
                break;
            case KillType.Headshot:
                AddMoney = 250;
                break;
            case KillType.Explosion:
                AddMoney = 550;
                break;

                // Doublekill - 200 
                // TripleKill - 300 
        }

        AnimationMoney(6, AddMoney);
    }

    public void AnimationMoney(int count = 1, int addMoney = 100)
    {
        bool isPlus = false;
        int scatter = 60;
        Vector3[] vector3Path =
        {
            path[0].transform.position,
            path[1].transform.position,
            path[2].transform.position,
            path[3].transform.position,
        };

        for (int i = 0; i < count; i++)
        {
            GameObject money = Instantiate(moneyPrefab, startPoint.transform.position, startPoint.transform.rotation, startPoint);
            money.transform.DOMove(money.transform.position + new Vector3(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter)), 0.7f * Time.timeScale).OnComplete(() =>
            {
                money.transform.DOPath(vector3Path, 0.5f * Time.timeScale).OnComplete(() =>
                {
                    if (!isPlus)
                    {
                        this.money += addMoney;
                        moneyText.text = this.money.ToString();
                        isPlus = true;
                    }

                    Destroy(money);
                });
            });
        }



        PlayerPrefs.SetInt("money", this.money);
    }
}
