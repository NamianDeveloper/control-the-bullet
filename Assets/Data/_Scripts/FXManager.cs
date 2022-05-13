using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] allFX;
    public void PlayFX(int idFX)
    {
        allFX[idFX].Play();
    }
}
