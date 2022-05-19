using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletCollisions : MonoBehaviour
{
    [SerializeField] private float ImpactStrength;
    private BulletMode bulletMode;
    private void Start()
    {
        bulletMode = GetComponent<BulletMode>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            DeleteBullet();
        }
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<PartOfRagdoll>(out PartOfRagdoll partOfRagdoll))
            {    
                GameManager.Instance.DeleteEnemy(partOfRagdoll.RagdollController.gameObject);
                partOfRagdoll.Rigidbody.AddForce(transform.forward * (100 * ImpactStrength));

                partOfRagdoll.RagdollController.EnablePhysics(true);

                if (bulletMode.CurrentFireMode == FireMode.OneShotOneKill)
                {
                    DeleteBullet();
                }
            }
        }
    }

    private void DeleteBullet()
    {
        CameraController.Instance.ResetTarget(true);
        Destroy(gameObject);
    }
}
