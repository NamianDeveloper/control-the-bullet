using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletMode : MonoBehaviour
{
    public FireMode CurrentFireMode => currentFireMode;

    private FireMode currentFireMode;
    void Start()
    {
        currentFireMode = ChangeFireModeButton.CurrentFireMode;
        Debug.Log("firemode - " + currentFireMode);
        if (currentFireMode == FireMode.PiercingShot)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(30 * Time.timeScale))
            .TakeUntilDestroy(gameObject)
            .TakeUntilDisable(gameObject)
            .Subscribe(_ =>
            {
                Debug.Log("Прошло 30 секунд");
            });
        }
    }
}
public enum FireMode
{
    OneShotOneKill,
    PiercingShot
}