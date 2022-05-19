using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShootController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletCasePrefab;

    [Header("Spawn Position")]
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform spawnBulletCasePosition;

    private FXManager managerFX;

    private void Start()
    {
        managerFX = GetComponent<FXManager>();
    }
    public void Fire()
    {
        managerFX.PlayFX(0);
        UiController.Instance.ShowUiElements(false);

        GameObject bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, spawnBulletPosition.rotation, gameObject.transform);

        TimeManager.Instance.SlowTime(true);

        Observable.Timer(System.TimeSpan.FromSeconds(TimeManager.Instance.SecondBeforeControlBullet * Time.timeScale))
        .Subscribe(_ =>
        {
            TimeManager.Instance.SlowTime(false);
            CameraController.Instance.NewTarget(bullet.transform);
        });
    }
}
