using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float intensity = 20;
    private Rigidbody rigidbody;
    private bool canMove;
    public bool CanMove
    {
        get => canMove;
        set { canMove = true; }
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbody.velocity = gameObject.transform.forward * 3;

        if (Input.GetMouseButton(0) && canMove)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            Vector3 newRotate = new Vector3(-y * intensity, x * intensity, 0);

            transform.Rotate(newRotate);
        }
    }
}
