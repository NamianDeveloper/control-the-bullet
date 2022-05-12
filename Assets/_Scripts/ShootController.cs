using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShootController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletCasePrefab;

    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform spawnBulletCasePosition;

    [SerializeField] private CameraController cameraController;
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, spawnBulletPosition.rotation, gameObject.transform);

        SpawnBulletCase();

        TimeManager.Instance.SlowTime(true);

        Observable.Timer(System.TimeSpan.FromSeconds(TimeManager.Instance.SecondBeforeControlBullet * Time.timeScale)).Subscribe(_ =>
        {
            TimeManager.Instance.SlowTime(false);
            cameraController.NewTarget(bullet.transform);
            if(bullet.TryGetComponent<BulletController>(out BulletController bulletController))
            {
                bulletController.CanMove = true;
            }
        });
    }
    private void SpawnBulletCase()
    {
        GameObject bulletCase = Instantiate(bulletCasePrefab, spawnBulletCasePosition.position, spawnBulletCasePosition.rotation, gameObject.transform);
        Destroy(bulletCase, 1f);
    }
}
