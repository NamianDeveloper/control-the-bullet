using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class EnemyController : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.AddEnemy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Observable.Timer(System.TimeSpan.FromSeconds(1))
            .TakeUntilDestroy(other.gameObject)
            .Subscribe(_ =>
            {
                GameManager.Instance.DeleteEnemy(this.gameObject);
                Destroy(gameObject);
            });
        }
    }
}
