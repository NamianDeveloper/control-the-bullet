using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShootController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Spawn Position")]
    [SerializeField] private Transform spawnBulletPosition;

    [SerializeField] private BulletCountManager bulletCountManager;

    private FXManager managerFX;

    private void Start()
    {
        managerFX = GetComponent<FXManager>();
    }
    public void Fire()
    {
        if (bulletCountManager.BulletCount > 0)
        {
            CameraController.Instance.MoveShootCamera(); 
            UiController.Instance.ShowUiElements(false);

            Observable.Timer(System.TimeSpan.FromSeconds(0.5f * Time.timeScale)).Subscribe(_ =>
            {
                managerFX.PlayFX(0);

                GameObject bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, spawnBulletPosition.rotation, gameObject.transform);

                BulletMoveController bulletMoveController = bullet.GetComponent<BulletMoveController>();

                bulletMoveController.BulletSpeed = 120;

                TimeManager.Instance.SlowTime(true);

                Observable.Timer(System.TimeSpan.FromSeconds(TimeManager.Instance.SecondBeforeControlBullet * Time.timeScale))
                .Subscribe(_ =>
                {
                    TimeManager.Instance.SlowTime(false);
                    CameraController.Instance.NewTarget(bullet.transform);
                    bulletMoveController.BulletSpeed = 10;
                });
                bulletCountManager.DeleteBullet();
            });
        }
    }
}
