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
    private List<GameObject> bulletIcons;
    void Start()
    {
        Init();
    }

    public void DeleteBullet(int deleteBullet = 1)
    {
        for (int i = 0; i < deleteBullet; i++)
        {
            bulletCount--;
            bulletIcons[bulletCount].GetComponent<Image>().color = new Color32(0, 0, 0, 128);
        }
        Debug.Log(bulletCount);
    }

    private void Init()
    {
        bulletCount = maxBulletCount;
        bulletIcons = new List<GameObject>();
        Vector3 startPos = new Vector3(0, 0, 0);
        for (int i = 0; i < maxBulletCount; i++)
        {
            GameObject icon = Instantiate(bulletIconPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            icon.transform.localPosition = startPos;
            bulletIcons.Add(icon);
            startPos.y -= 100;
        }
    }
}
