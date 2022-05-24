using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] AllRigidbodys;
    [SerializeField] private GameObject mark;
    private void Start()
    {
        EnablePhysics(false);
    }

    public void EnablePhysics(bool status)
    {
        mark.SetActive(!status);
        for (int i = 0; i < AllRigidbodys.Length; i++)
        {
            AllRigidbodys[i].isKinematic = !status;
        }
    }
}
