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
        moneyText.text = money + "$";
    }

    public void AddMoney(KillType killType)
    {
        switch (killType)
        {
            case KillType.Body: 
                money += 100;
                break;
            case KillType.Headshot:
                money += 200;
                break;
            case KillType.Explosion:
                money += 300;
                break;
        }
    
        moneyText.text = money + "$";
        PlayerPrefs.SetInt("money", money);
    }
}
