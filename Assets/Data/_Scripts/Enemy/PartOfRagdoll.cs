using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfRagdoll : MonoBehaviour
{
    [SerializeField] private RagdollController ragdollController;
    [SerializeField] private bool isHead;
    public Rigidbody Rigidbody;
    public RagdollController RagdollController => ragdollController;
    public KillType PartOfKill => isHead ? KillType.Headshot : KillType.Body;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
