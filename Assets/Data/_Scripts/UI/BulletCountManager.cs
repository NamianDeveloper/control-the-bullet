using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountManager : MonoBehaviour
{
    [SerializeField] private int maxBulletCount;
    [SerializeField] private GameObject bulletIconPrefab;

    [SerializeField] private Transform spawnPoint;
    public int BulletCount => bulletCount;

    private int bulletCount;
    [SerializeField] private List<GameObject> bulletIcons;
    void Start()
    {
        bulletCount = maxBulletCount;
    }

    public void DeleteBullet(int deleteBullet = 1)
    {
        for (int i = 0; i < deleteBullet; i++)
        {
            bulletCount--;
            bulletIcons[bulletCount].GetComponent<Image>().color = new Color32(1, 1, 1, 100);
        }
    }
}
