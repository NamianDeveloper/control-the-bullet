using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(-2, 2)] private float ofsetY;
    [SerializeField] private ShutterAnimator shutterAnimator;

    public static CameraController Instance;

    private Transform target;
    private Vector3 startPos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        startPos = transform.position;
    }
    public void NewTarget(Transform targets)
    {
        target = targets;
        gameObject.transform.SetParent(targets);
        SetCamera(new Vector3(0, ofsetY, -6), new Vector3(), 0.4f, targets);
    }

    public void ResetTarget(bool showUI = false)
    {
        target = null;
        gameObject.transform.SetParent(null);

        gameObject.transform.DOLocalRotate(new Vector3(15, -20, 0), 0.4f);
        gameObject.transform.DOLocalMove(startPos, 0.4f)
        .OnComplete(() =>
        {
            if (showUI)
            {

                UiController.Instance.ShowUiElements();
            }
            shutterAnimator.RetractShutter();

        });
    }
    private void SetCamera(Vector3 doMove, Vector3 doRotate, float time, Transform bulletTarget = null)
    {
        gameObject.transform.DOLocalRotate(new Vector3(), time);
        gameObject.transform.DOLocalMove(doMove, time)
            .OnComplete(() =>
            {
                if (bulletTarget)
                {
                    if (target.TryGetComponent<BulletMoveController>(out BulletMoveController bulletController))
                    {
                        bulletController.CanMove = true;
                    }
                }
            });
    }
}
