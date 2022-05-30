using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] AllRigidbodys;
    [SerializeField] private GameObject mark;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private GameObject ExplosionCollider;
    public EnemyController EnemyController
    {
        get;
        set;
    }

    private EnemyMove enemyMove;
    public bool IsDead { get; set; }

    public GameObject point;
    private void Start()
    {
        EnemyController = GetComponent<EnemyController>();
        EnablePhysics(false);
        enemyMove = GetComponent<EnemyMove>();
    }

    public void EnablePhysics(bool status)
    {
        mark.SetActive(!status);
        ExplosionCollider.SetActive(!status);
        for (int i = 0; i < AllRigidbodys.Length; i++)
        {
            AllRigidbodys[i].isKinematic = !status;
        }

        if (status)
        {
            enemyMove.KillDOTween();
            ShowBlood();
        }

    }
    private void ShowBlood()
    {
        blood.Play();
    }
}
