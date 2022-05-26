using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BulletMoveController : MonoBehaviour
{
    [SerializeField, Space] private float intensity = 2;
    [SerializeField, Range(0, 15)] private float bulletSpeed = 7;
    [SerializeField] private float maxTiltAngle;

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

    public float BulletSpeed
    {
        get => bulletSpeed;
        set { bulletSpeed = value; }
    }

    private float secondMaxTiltAngle;

    private Rigidbody rigidbody;

    private bool canMove;
    private bool canRotateX;

    void Start()
    {
        canRotateX = true;
        rigidbody = GetComponent<Rigidbody>();

        secondMaxTiltAngle = 360 - maxTiltAngle;
    }
    void FixedUpdate()
    {
        rigidbody.velocity = gameObject.transform.forward * bulletSpeed;

        if (Input.GetMouseButton(0) && canMove)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            if (!((transform.rotation.eulerAngles.x < maxTiltAngle && transform.rotation.eulerAngles.x > -maxTiltAngle) || (transform.rotation.eulerAngles.x > secondMaxTiltAngle && transform.rotation.eulerAngles.x > -secondMaxTiltAngle)))
            {
                if (transform.rotation.eulerAngles.x > maxTiltAngle && transform.rotation.eulerAngles.x < maxTiltAngle + 30 && -y < 0)
                {
                    canRotateX = true;
                }
                else if (transform.rotation.eulerAngles.x < secondMaxTiltAngle && transform.rotation.eulerAngles.x > secondMaxTiltAngle - 30 && -y > 0)
                {
                    canRotateX = true;
                }
                else
                {
                    canRotateX = false;
                }
            }

            if (canRotateX) transform.Rotate(transform.InverseTransformDirection(transform.right), -y * intensity);
            transform.Rotate(transform.InverseTransformDirection(Vector3.up), x * intensity);
        }
    }
}