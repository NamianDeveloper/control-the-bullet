using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
public class TapToStartAnimation : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1).onComplete();
    }
}
