using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfGlass : MonoBehaviour
{
    [SerializeField] private GlassController glassController;
    public Rigidbody Rigidbody;
    public GlassController GlassController => glassController;

    public GlassController GlassController1 { get { return glassController; } }
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
