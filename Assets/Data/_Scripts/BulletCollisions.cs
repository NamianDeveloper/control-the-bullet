using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            CameraController.Instance.ResetTarget(true);
            Destroy(gameObject);
        }
    }
}
