using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class FireBarrelController : MonoBehaviour
{
    [SerializeField] private float Radius;

    [SerializeField] private ParticleSystem explosion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
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
