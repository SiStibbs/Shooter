using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Spawns enemies into a pool
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public int poolSize;
    public GameObject enemyPrefab;
    public List<GameObject> enemies;

    void Awake()
    {
        enemies = new List<GameObject>();
        GameObject tmp;
        for(int i=0; i<poolSize; i++)
        {
            tmp = Instantiate(enemyPrefab);
            tmp.SetActive(false);
            enemies.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i=0; i<poolSize; i++)
        {
            if(! enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }
        }

        return null;
    }

    public void DespawnAll()
    {
        for (int i = 0; i < poolSize; i++)
        {
            enemies[i].SetActive(false);
        }
    }
}
