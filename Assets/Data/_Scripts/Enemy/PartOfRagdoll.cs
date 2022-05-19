using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfRagdoll : MonoBehaviour
{
    [SerializeField] private RagdollController ragdollController;
    public Rigidbody Rigidbody;
    public RagdollController RagdollController => ragdollController;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
