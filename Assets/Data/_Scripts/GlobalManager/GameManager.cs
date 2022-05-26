using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI enemyCount;
    [SerializeField] private BulletCountManager bulletCountManager;
    [SerializeField] private UiController uiController;

    [SerializeField] private List<GameObject> enemy;

    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

    private int killEnemyCount;
    private int MaxEnemyCount;
    private bool winGame;
    private void Awake()
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
    public void AddEnemy(GameObject enemy)
    {
        this.enemy.Add(enemy);
        enemyCount.text = killEnemyCount + "/" + this.enemy.Count;
        MaxEnemyCount++;
    }
    public void DeleteEnemy(GameObject enemy, KillType killType = KillType.Body)
    {
        UiController.Instance.ShowMessageKill(killType);
        MoneyController.Instance.AddMoney(killType);
        killEnemyCount++;
        this.enemy.Remove(enemy);
        enemyCount.text = killEnemyCount + "/" + MaxEnemyCount;
        TryFinishGame();
    }
    private void TryFinishGame()
    {
        if (enemy.Count == 0)
        {
            CameraController.Instance.ResetTarget();
            UiController.Instance.ShowUiElements(false);
            TimeManager.Instance.ResetSlowTime();
            winGame = true;
            winScreen.SetActive(true);
        }
    }
    public void OnEndBullet()
    {
        if (bulletCountManager.BulletCount == 0 && !winGame)
        {
            ShowScreen(false);
        }
    }

    private void ShowScreen(bool statusGame)
    {
        UiController.Instance.ShowUiElements(false);
        Observable.Timer(System.TimeSpan.FromSeconds(3 * Time.deltaTime)).Subscribe(_ =>
        {
            if (statusGame)
            {
                winScreen.SetActive(true);
            }
            else
            {
                loseScreen.SetActive(true);
            }
        });
    }
}
public enum KillType
{
    Headshot,
    Body,
    Explosion
}
