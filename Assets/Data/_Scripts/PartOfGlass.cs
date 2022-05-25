using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfGlass : MonoBehaviour
{
    [SerializeField] private GlassController glassController;
    public Rigidbody Rigidbody;
    public GlassController GlassController => glassController;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
