using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float timeScaleBeforeFire;
    [SerializeField, Range(0, 1)] private float timeScaleAfterFire;
    [SerializeField, Range(0, 10)] private float secondBeforeControlBullet;

    public static TimeManager Instance;

    public float SecondBeforeControlBullet => secondBeforeControlBullet;

    void Start()
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
    public void SlowTime(bool slowModeBeforeFire)
    {
        if (slowModeBeforeFire)
            Time.timeScale = timeScaleBeforeFire;
        else
            Time.timeScale = timeScaleAfterFire;
    }
}
