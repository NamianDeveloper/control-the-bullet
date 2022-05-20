using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandAnimation : MonoBehaviour
{
    [SerializeField] private int Time;


    [SerializeField, Space] private int middlePositionX;
    [SerializeField] private int MaxPositionX;
    [SerializeField] private int MaxPositionY;

    void Start()
    {
        Vector3[] path = new Vector3[]
        {
            new Vector3(),//START POINT
            new Vector3(-middlePositionX,-MaxPositionY),
            new Vector3(-MaxPositionX, 0),
            new Vector3(-middlePositionX, MaxPositionY),

            new Vector3(middlePositionX,-MaxPositionY), 
            new Vector3(MaxPositionX, 0), 
            new Vector3(middlePositionX, MaxPositionY),
            new Vector3() //END POINT
        };
        transform.DOLocalPath(path, Time, PathType.Linear).SetEase(Ease.Linear).SetLoops(-1);
    }
}
