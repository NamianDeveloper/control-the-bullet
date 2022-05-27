using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLink : MonoBehaviour
{
    public EnemyController EnemyController => enemyController;
    public BoxCollider BoxCollider => boxCollider;

    [SerializeField] private EnemyController enemyController;
    [SerializeField] private BoxCollider boxCollider;
}
