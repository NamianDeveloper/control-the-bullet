using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] AllRigidbodys;
    private void Start()
    {
        EnablePhysics(false);
    }

    public void EnablePhysics(bool status)
    {
        for (int i = 0; i < AllRigidbodys.Length; i++)
        {
            AllRigidbodys[i].gameObject.SetActive(status);
            AllRigidbodys[i].isKinematic = !status;
        }
        if (status) gameObject.SetActive(false);

    }
}
