using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(-2, 2)] private float ofsetY;
    [SerializeField] private ShutterAnimator shutterAnimator;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject[] allCameraPosition;

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
        startPos = transform.localPosition;
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
        gameObject.transform.SetParent(player);

        gameObject.transform.DOLocalRotate(new Vector3(15, -20, 0), 0.4f);
        gameObject.transform.DOLocalMove(startPos, 0.4f)
        .OnComplete(() =>
        {
            if (showUI)
            {

                UiController.Instance.ShowUiElements();
            }
            shutterAnimator.RetractShutter();
            GameManager.Instance.OnEndBullet();
            TimeManager.Instance.ResetSlowTime();
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

    public void MoveShootCamera(bool isStart = false)
    {
        int currentPosition = Random.Range(0, allCameraPosition.Length);
        if (isStart) currentPosition = 0;
        Debug.Log(transform.eulerAngles + "/" + allCameraPosition[currentPosition].transform.eulerAngles);
        if (isStart)
        {
            Debug.Log("Сейчас на " + transform.position);
            Debug.Log("Долэен перемещаться к " + allCameraPosition[0].transform.position);
            transform.DOMove(allCameraPosition[0].transform.position, 0.5f * Time.timeScale);
            transform.DORotate(allCameraPosition[0].transform.eulerAngles, 0.5f * Time.timeScale);
        }
        else
        {
            transform.DOMove(allCameraPosition[currentPosition].transform.position, 0.5f * Time.timeScale);
            transform.DORotate(allCameraPosition[currentPosition].transform.eulerAngles, 0.5f * Time.timeScale);
        }

    }
}
