using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Spawns bullets into a pool
/// </summary>
public class BulletSpawner : MonoBehaviour
{
    public int poolSize = 10;
    public GameObject bulletPrefab;
    public List<GameObject> bullets;

    void Start()
    {
        bullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolSize; i++)
        {
            tmp = Instantiate(bulletPrefab);
            //tmp.transform.parent = transform;
            tmp.SetActive(false);
            bullets.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = transform.position;
                bullets[i].transform.rotation = transform.rotation;
                return bullets[i];
            }
        }

        return null;
    }
}
