using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            CameraController.Instance.ResetTarget(true);
            Destroy(gameObject);
        }
    }
}
