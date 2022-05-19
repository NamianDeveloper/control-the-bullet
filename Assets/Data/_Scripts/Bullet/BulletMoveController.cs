using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BulletMoveController : MonoBehaviour
{
    [SerializeField] private float intensity = 20;
    [SerializeField, Range(0, 8)] private float bulletSpeed = 3;

    [SerializeField] private ParticleSystem wind;
    public bool CanMove
    {
        get => canMove;
        set 
        {
            canMove = value;
            wind.Play();
        }
    } 

    private Rigidbody rigidbody;
    private bool canMove;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbody.velocity = gameObject.transform.forward * bulletSpeed;

        if (Input.GetMouseButton(0) && canMove)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            Vector3 newRotate = new Vector3(-y * intensity, x * intensity, 0);

            transform.Rotate(newRotate);
        }
    }
}