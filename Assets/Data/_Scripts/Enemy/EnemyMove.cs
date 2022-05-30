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
        path = new Vector3[transformPath.Length];

        for (int i = 0; i < path.Length; i++)
        {
            path[i] = transformPath[i].position;
        }
        if (isStatic) return;

        animator.Play("Move");
        tween = gameObject.transform.DOPath(path, (timeToMove * (path.Length - 1)) * Time.timeScale)
        .OnWaypointChange(_ =>
        {
            RotateCharacter(_);
            tween.Pause();
        })
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            if (isLoopMoving) tween.Restart();
        });
    }


    private void RotateCharacter(int id)
    {
        transform.DOLookAt(path[id + 1], timeToRotate, AxisConstraint.Y, Vector3.up).OnComplete(() =>
        {
            tween.Play();
        });
    }
    public void KillDOTween()
    {
        rotationTween?.Kill();
        tween?.Kill();
        animator.runtimeAnimatorController = null;
    }
}
