using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class EnemyController : MonoBehaviour
{
    private RagdollController ragdollController;

    public bool isDead { get; set; }
    void Start()
    {
        ragdollController = GetComponent<RagdollController>();

        GameManager.Instance.AddEnemy(this.gameObject);
    }

    public void DeleteEnemy(KillType kiilType)
    {
        if (isDead) return;
        ragdollController.EnablePhysics(true);
        GameManager.Instance.DeleteEnemy(this.gameObject, kiilType);

    }
}
