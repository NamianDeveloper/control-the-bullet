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

    [SerializeField] private float time;

    public bool IsDead { get;  set; }

    public GameObject point;
    private void Start()
    {
        EnemyController = GetComponent<EnemyController>();
        EnablePhysics(false);
    }

    public void EnablePhysics(bool status, Vector3 pointOfImpact = new Vector3())
    {
        mark.SetActive(!status);
        ExplosionCollider.SetActive(!status);
        ShowBlood();
        for (int i = 0; i < AllRigidbodys.Length; i++)
        {
            AllRigidbodys[i].isKinematic = !status;
        }
    }
    private void ShowBlood()
    {
        blood.Play();
    }
}
