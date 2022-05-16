using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<GameObject> enemy;
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
        Debug.Log(this.enemy.Count);
    }
    public void DeleteEnemy(GameObject enemy)
    {
        this.enemy.Remove(enemy);
        TryFinishGame();
    }

    private void TryFinishGame()
    {
        if (enemy.Count == 0)
        {
            CameraController.Instance.ResetTarget();
        }
    }
}
