using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] AllRigidbodys;
    [SerializeField] private GameObject mark;
    [SerializeField] private ParticleSystem blood;

    [SerializeField] private float time;

    public GameObject point;
    private void Start()
    {
        EnablePhysics(false);
    }

    public void EnablePhysics(bool status, Vector3 pointOfImpact = new Vector3())
    {
        mark.SetActive(!status);
        Debug.Log("����� � " + pointOfImpact);
        ShowBlood();
        for (int i = 0; i < AllRigidbodys.Length; i++)
        {
            AllRigidbodys[i].isKinematic = !status;
        }
    }
    private void ShowBlood()
    {

        Observable.Timer(System.TimeSpan.FromSeconds(time * Time.timeScale)).TakeUntilDestroy(gameObject).TakeUntilDisable(gameObject).Subscribe(_ => { blood.Play(); });
       
    }
}
