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
            if (false)
            {
                Collider[] playerCollider = Physics.OverlapSphere(transform.position, Radius, LayerMask.GetMask("Enemy"));

                Debug.Log(playerCollider.Length);

                foreach (Collider coll in playerCollider)
                {
                    if (coll.gameObject.TryGetComponent<EnemyLink>(out EnemyLink enemylink))
                    {
                        enemylink.EnemyController.DeleteEnemy(KillType.Explosion);
                        Destroy(enemylink.BoxCollider);
                    }
                }

            }
            else
            {
                Collider[] playerCollider = Physics.OverlapSphere(transform.position, Radius, LayerMask.GetMask("Enemy", "Glass", "Scaffolding"));
                Debug.Log(playerCollider.Length);

                foreach (Collider coll in playerCollider)
                {
                    if (coll.gameObject.TryGetComponent<EnemyLink>(out EnemyLink enemylink))
                    {
                        enemylink.EnemyController.DeleteEnemy(KillType.Explosion);
                        Destroy(enemylink.BoxCollider);
                    }

                    else if (coll.gameObject.TryGetComponent<GlassController>(out GlassController glassController))
                    {
                        glassController.EnablePhysics(true);
                    }
                    else if (coll.gameObject.TryGetComponent<ScafFoldingController>(out ScafFoldingController scafFoldingController))
                    {
                        scafFoldingController.EnablePhysics(true);
                    }
                }
                Collider[] allCollider = Physics.OverlapSphere(transform.position, Radius);

                foreach (Collider coll in allCollider)
                {               
                    if (coll.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    {
                        Debug.Log($"У {coll.gameObject.name} есть Риджидбоди");
                        rigidbody.AddExplosionForce(600, gameObject.transform.position, Radius, 3);
                    }
                }
            }
            explosion.Play();
            Destroy(gameObject);
        }
    }
}
