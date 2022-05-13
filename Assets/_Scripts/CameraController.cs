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
        SetCamera(target.gameObject);
    }

    private void SetCamera(GameObject target)
    {
        gameObject.transform.DORotate(new Vector3(), 0.5f);
        gameObject.transform.DOLocalMove(new Vector3(0, ofsetY, -4), 0.5f)
            .OnComplete(() =>
            {
                if (target.TryGetComponent<BulletController>(out BulletController bulletController))
                {
                    bulletController.CanMove = true;
                }
            });
    }
}
