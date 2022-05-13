using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShutterAnimator : MonoBehaviour
{
    [SerializeField] private GameObject shutter;
    [SerializeField, Range(0, 1)] private float maxPosX;
    public void RetractShutter()
    {
        Vector3 startPos = shutter.transform.localPosition;
        Vector3 newPos = new Vector3(maxPosX, shutter.transform.localPosition.y, shutter.transform.localPosition.z); 
        shutter.transform.DOLocalMove(newPos, 1)
            .OnComplete(() => 
            {
                shutter.transform.DOLocalMove(startPos, 1);
            });
    }
}
