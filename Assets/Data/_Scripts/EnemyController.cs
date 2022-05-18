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

    public void DeleteEnemy()
    {
        ragdollController.EnablePhysics(true);
        GameManager.Instance.DeleteEnemy(this.gameObject);

    }
}
