using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BulletMoveController : MonoBehaviour
{
    [SerializeField, Space] private float intensity = 20;
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

            if (!((transform.rotation.eulerAngles.x < 40 && transform.rotation.eulerAngles.x > -40) || (transform.rotation.eulerAngles.x > 320 && transform.rotation.eulerAngles.x > -320)))
            {
                if (transform.rotation.eulerAngles.x > 40 && transform.rotation.eulerAngles.x < 70 && -y < 0)
                {
                    transform.Rotate(transform.InverseTransformDirection(transform.right), -y * intensity / 6);
                }
                else if (transform.rotation.eulerAngles.x < 320 && transform.rotation.eulerAngles.x > 290 && -y > 0)
                {
                    transform.Rotate(transform.InverseTransformDirection(transform.right), -y * intensity / 6);
                }
            }
            else
            {
                transform.Rotate(transform.InverseTransformDirection(transform.right), -y * intensity / 6);
            }

            transform.Rotate(transform.InverseTransformDirection(Vector3.up), x * intensity / 6);
        }
    }
}