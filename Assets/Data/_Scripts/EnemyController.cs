using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class EnemyController : MonoBehaviour
{
    private RagdollController ragdollController;

    void Start()
    {
        ragdollController = GetComponent<RagdollController>();

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
        ragdollController.EnablePhysics(true);
        GameManager.Instance.DeleteEnemy(this.gameObject);

    }
}
