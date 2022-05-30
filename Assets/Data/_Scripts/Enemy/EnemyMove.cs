using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private bool isStatic;
    [SerializeField] private float timeToMove;
    [SerializeField] private float timeToRotate;
    [SerializeField] private bool isLoopMoving;
    //[SerializeField] private bool isLoopMoving;

    [Header("Other")]
    [SerializeField] private Transform[] transformPath;
    [SerializeField] private Animator animator;

    private Vector3[] path;

    private Tween tween;
    private Tween rotationTween;
    void Start()
    {
        path = new Vector3[transformPath.Length * 2 - 1];

        for (int i = 0; i < transformPath.Length; i++)
        {
            path[i] = transformPath[i].position;
            Debug.Log($"path[{i}] = transformPath[{i}].position;");
        }
        for (int i = 1; i < transformPath.Length; i++)
        {
            Debug.Log($"path[{path.Length - i}] = transformPath[{i - 1}].position;");
            path[path.Length - i] = transformPath[i - 1].position;

        }
        if (isStatic) return;

        animator.Play("Move");
        Debug.Log(Time.timeScale);
        tween = gameObject.transform.DOPath(path, (timeToMove * (path.Length - 1)))
        .OnWaypointChange(_ =>
        {
            RotateCharacter(_);
           // tween.Pause();
        })
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {

        }).SetLoops(-1);
    }


    private void RotateCharacter(int id)
    {
        transform.DOLookAt(path[id + 1], timeToRotate, AxisConstraint.Y, Vector3.up).OnComplete(() =>
        {
          //  tween.Play();
        });
    }
    public void KillDOTween()
    {
        rotationTween?.Kill();
        tween?.Kill();
        animator.runtimeAnimatorController = null;
    }
}
