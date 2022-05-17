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
            DeleteEnemy();
        }
    }

    public void DeleteEnemy()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(1))
           .Subscribe(_ =>
           {
               GameManager.Instance.DeleteEnemy(this.gameObject);
               Destroy(gameObject);
           });
    }
}
