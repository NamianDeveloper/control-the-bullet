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

    [Header("Links to scripts")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private ShutterAnimator shutterAnimator;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, spawnBulletPosition.rotation, gameObject.transform);

        shutterAnimator.RetractShutter();
        SpawnBulletCase();

        TimeManager.Instance.SlowTime(true);

        Observable.Timer(System.TimeSpan.FromSeconds(TimeManager.Instance.SecondBeforeControlBullet * Time.timeScale)).Subscribe(_ =>
        {
            TimeManager.Instance.SlowTime(false);
            cameraController.NewTarget(bullet.transform);
        });
    }
    private void SpawnBulletCase()
    {
        GameObject bulletCase = Instantiate(bulletCasePrefab, spawnBulletCasePosition.position, spawnBulletCasePosition.rotation, gameObject.transform);
        Destroy(bulletCase, 1f);
    }
}
