using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class FireBarrelController : MonoBehaviour
{
    [SerializeField] private float Radius;

    [SerializeField] private ParticleSystem explosion;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log($"В радиусе {Radius} все умрет");
            Collider[] playerCollider = Physics.OverlapSphere(transform.position, Radius, LayerMask.GetMask("Enemy"));
      
            foreach (Collider coll in playerCollider)
            {
                if (coll.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
                {
                    enemyController.DeleteEnemy();
                }
            }
            explosion.Play();
        }
    }
}
