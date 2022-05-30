using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    [SerializeField, Range(-2, 2)] private float ofsetY;
    [SerializeField] private ShutterAnimator shutterAnimator;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject[] allCameraPosition;

    [SerializeField] private Image darkImage;

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
        ShowDarkImage();


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
        if (isStart)
        {
            transform.DOMove(allCameraPosition[0].transform.position, 0.5f * Time.timeScale);
            transform.DORotate(allCameraPosition[0].transform.eulerAngles, 0.5f * Time.timeScale).OnComplete(() =>
            {
                UiController.Instance.ShowUiElements();
                GameManager.Instance.OnEndBullet();
            });
        }
        else
        {
            transform.DOMove(allCameraPosition[currentPosition].transform.position, 0.5f * Time.timeScale);
            transform.DORotate(allCameraPosition[currentPosition].transform.eulerAngles, 0.5f * Time.timeScale);
        }

    }

    private void ShowDarkImage()
    {
        float time = 0.5f * Time.timeScale;
        int currentPosition = Random.Range(0, allCameraPosition.Length);



        darkImage.DOColor(new Color(0, 0, 0, 1), time)
        .OnComplete(() =>
        {

            transform.position = allCameraPosition[currentPosition].transform.position;
            transform.rotation = new Quaternion(
                allCameraPosition[currentPosition].transform.rotation.x,
                allCameraPosition[currentPosition].transform.rotation.y,
                allCameraPosition[currentPosition].transform.rotation.z,
                allCameraPosition[currentPosition].transform.rotation.w);

            shutterAnimator.RetractShutter();

            TimeManager.Instance.ResetSlowTime();


            darkImage.DOColor(new Color(0, 0, 0, 0), time);
        });
    }
}
