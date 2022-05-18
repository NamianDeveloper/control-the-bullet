using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletCollisions : MonoBehaviour
{
    [SerializeField] private float ImpactStrength;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            CameraController.Instance.ResetTarget(true);
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<PartOfRagdoll>(out PartOfRagdoll partOfRagdoll))
            {    
                GameManager.Instance.DeleteEnemy(other.gameObject);
                partOfRagdoll.Rigidbody.AddForce(transform.forward * (100 * ImpactStrength));

                partOfRagdoll.RagdollController.EnablePhysics(true);
            }
        }
    }

}
