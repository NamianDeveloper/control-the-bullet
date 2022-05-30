using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform[] transformPath;
     private Vector3[] path;
    void Start()
    {
        path = new Vector3[transformPath.Length];

        for (int i = 0; i < path.Length; i++)
        {
            path[i] = transformPath[i].position;
        }

        gameObject.transform.DOPath(path, 5)
        .OnWaypointChange(_ =>
        {
            RotateCharacter(_);
        })
        .SetEase(Ease.Linear)
        .SetLoops(-1);
    }

    private void RotateCharacter(int id)
    {
        //  Vector3 angle = transform.rotation.eulerAngles - transformPath[id + 1].rotation.eulerAngles;
        Vector3 angle = transform.position - transformPath[id + 1].transform.position;
        angle.x = 0;
        angle.z = 0;
        //   Vector3 angle = transform.position - transformPath[id + 1].position;
        Debug.Log(angle);
        transform.DORotate(angle, 0.4f); 
        Debug.Log(id + " Шаг пройден");
      //  transform.LookAt(transformPath[id], Vector3.up);
    }
}
