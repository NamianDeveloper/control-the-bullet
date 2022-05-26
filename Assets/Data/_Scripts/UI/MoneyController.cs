using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public static MoneyController Instance;

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
      if(PlayerPrefs.HasKey("money")) money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString();
    }

    public void AddMoney(KillType killType)
    {
        switch (killType)
        {
            case KillType.Body: 
                money += 100;
                break;
            case KillType.Headshot:
                money += 250;
                break;
            case KillType.Explosion:
                money += 550;
                break;

                // Doublekill - 200 
                // TripleKill - 300 
        }

        moneyText.text = money.ToString();
        PlayerPrefs.SetInt("money", money);
    }
}
