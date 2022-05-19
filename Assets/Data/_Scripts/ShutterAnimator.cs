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
        playerAnimator.Play("Reload");
        Observable.Timer(System.TimeSpan.FromSeconds(delay * Time.timeScale))
            .TakeUntilDestroy(gameObject)
            .TakeUntilDisable(gameObject)
            .Subscribe(_ => SpawnBulletCase());
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
