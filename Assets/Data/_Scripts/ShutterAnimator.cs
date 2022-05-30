using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class ShutterAnimator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletCasePrefab;
    [Header("Spawn Position")]
    [SerializeField] private Transform spawnBulletCasePosition;
    [Header("Links")]
    [SerializeField] private Animator playerAnimator;
    [Header("Setting")]
    [SerializeField] private float delay;

    private FXManager managerFX;
    private void Start()
    {
        managerFX = GetComponent<FXManager>();
    }
    public void RetractShutter()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(0.5f * Time.timeScale)).Subscribe(_ =>
        {
            playerAnimator.Play("Reload");
            UiController.Instance.TapToShot.SetActive(false);
            Observable.Timer(System.TimeSpan.FromSeconds(delay * Time.timeScale))
                .TakeUntilDestroy(gameObject)
                .TakeUntilDisable(gameObject)
                .Subscribe(_ => SpawnBulletCase());

            Observable.Timer(System.TimeSpan.FromSeconds(2 * Time.timeScale))
              .TakeUntilDestroy(gameObject)
              .TakeUntilDisable(gameObject)
              .Subscribe(_ => UiController.Instance.TapToShot.SetActive(true));

            Observable.Timer(System.TimeSpan.FromSeconds(1.5f * Time.timeScale)).Subscribe(_ =>
            {
                 CameraController.Instance.MoveShootCamera(true);
            });
        });


    }
    private void SpawnBulletCase()
    {
        managerFX.PlayFX(1);
        GameObject bulletCase = Instantiate(bulletCasePrefab, spawnBulletCasePosition.position, spawnBulletCasePosition.rotation, gameObject.transform);
        if (bulletCase.transform.TryGetComponent<Rigidbody>(out Rigidbody bulletCaseRigidbody))
        {
            bulletCaseRigidbody.AddForce(new Vector3(1, 1, -1), ForceMode.Impulse);
        }
        Destroy(bulletCase, 1f);
    }
}
