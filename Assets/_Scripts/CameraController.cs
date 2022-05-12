using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(-2, 2)] private float ofsetY;
    private Transform target;

    public void NewTarget(Transform targets)
    {
        target = targets;
        gameObject.transform.SetParent(targets);
        SetCamera();
    }

    private void SetCamera()
    {
        gameObject.transform.DOLocalMove(new Vector3(0, ofsetY, -4), 0.5f);
        gameObject.transform.DORotate(new Vector3(), 0.5f);
    }
}
