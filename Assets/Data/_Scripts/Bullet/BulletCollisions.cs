using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletCollisions : MonoBehaviour
{
    [SerializeField] private float ImpactStrength;
    private BulletMode bulletMode;
    private BulletMoveController bulletMoveController;
    private void Start()
    {
        bulletMode = GetComponent<BulletMode>();
        bulletMoveController = GetComponent<BulletMoveController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            DeleteBullet();
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<PartOfRagdoll>(out PartOfRagdoll partOfRagdoll))
            {
                if (partOfRagdoll.RagdollController.EnemyController.isDead) return;
                else partOfRagdoll.RagdollController.EnemyController.isDead = true;

                GameManager.Instance.DeleteEnemy(partOfRagdoll.RagdollController.gameObject, partOfRagdoll.PartOfKill);
                partOfRagdoll.Rigidbody.AddForce(transform.forward * (100 * ImpactStrength));

                partOfRagdoll.AddBlood(transform.position);

                partOfRagdoll.RagdollController.EnablePhysics(true, transform.position);

                bulletMoveController.BulletSpeed = 1;
                Observable.Timer(System.TimeSpan.FromSeconds(1) * Time.timeScale)
                    .TakeUntilDestroy(gameObject)
                    .TakeUntilDisable(gameObject)
                    .Subscribe(_ =>
                    {
                        bulletMoveController.BulletSpeed = 10;
                    });

                if (bulletMode.CurrentFireMode == FireMode.OneShotOneKill)
                {
                    DeleteBullet();
                }
            }
        }

        else if (other.CompareTag("Glass"))
        {
            if (other.TryGetComponent<GlassController>(out GlassController glassController))
            {
                glassController.EnablePhysics(true);
            }
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                Debug.Log(" упол - коллизии€€€€");
                rigidbody.AddForce(transform.forward * (100 * ImpactStrength));


                bulletMoveController.BulletSpeed = 1;
                Observable.Timer(System.TimeSpan.FromSeconds(1) * Time.timeScale)
                    .TakeUntilDestroy(gameObject)
                    .TakeUntilDisable(gameObject)
                    .Subscribe(_ =>
                    {
                        bulletMoveController.BulletSpeed = 10;
                    });
            }
        }
    }

    private void DeleteBullet()
    {
        CameraController.Instance.ResetTarget(true);
        Destroy(gameObject);
    }
}
