using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Controller for handling enemies, such as when to spawn them.
/// </summary>
public class EnemyController : MonoBehaviour
{
    public EnemySpawner spawner;
    public int spawnCount = 5;
    private GameObject playerObject;
    public EventListenerSimple targetHitEventListener;
    public EventListenerSimple playerHitEventListener;
    public EventListenerBool gameActiveEventListener;

    private float maxSpeed = 1.1f;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemies());
    }

    private void OnEnable()
    {
        targetHitEventListener.RegisterListener();
        targetHitEventListener.onEventRaisedAction = SpawnEnemyDelayed;

        playerHitEventListener.RegisterListener();
        playerHitEventListener.onEventRaisedAction = SpawnEnemyDelayed;

        gameActiveEventListener.RegisterListener();
        gameActiveEventListener.onEventRaisedAction = GameStatusChanged;
    }

    private void OnDisable()
    {
        targetHitEventListener.UnregisterListener();
        playerHitEventListener.UnregisterListener();
        gameActiveEventListener.UnregisterListener();
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(3);
        }
    }

    // <summary>
    /// Create an enemy and set its position to be roughly in front of the user.
    /// If no enemy is available from the pool, no nothing.
    /// </summary>
    public void SpawnEnemy()
    {
        GameObject enemyObj = spawner.GetPooledObject();
        if (enemyObj != null)
        {
            float randUp = Random.Range(0f, 3f);
            float randForw = Random.Range(6f, 10f);
            float randRight = Random.Range(-5f, 5f);

            Vector3 position = playerObject.transform.position
                + (Camera.main.transform.forward * randForw)
                + (Camera.main.transform.right * randRight);
            position.y = randUp;
            enemyObj.transform.position = position;

            Color randomColor = Random.ColorHSV();
            enemyObj.GetComponent<MeshRenderer>().material.SetColor("_color", randomColor);
            enemyObj.GetComponent<Enemy>().speed = Random.Range(.5f, maxSpeed);
            maxSpeed += 0.05f;

            enemyObj.SetActive(true);
        }
    }

    public void SpawnEnemyDelayed()
    {
        StartCoroutine(SpawnEnemyDelayedCR());
    }

    private IEnumerator SpawnEnemyDelayedCR()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        SpawnEnemy();
    }

    // <summary>
    /// Handle changes to the game status
    /// </summary>
    private void GameStatusChanged(bool isActive)
    {
        if (!isActive)
        {
            spawner.DespawnAll();
            StopAllCoroutines();
        }
        else
            StartCoroutine(SpawnEnemies());
    }
}
