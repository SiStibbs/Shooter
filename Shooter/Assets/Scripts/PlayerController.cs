using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Controller class for the Player.
/// Handles things like shooting bullets.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public BulletSpawner bulletSpawner;
    public int bulletSpeed = 100;
    public FloatVariable currentScore;
    public ParticleSystem muzzleFlash;

    public EventListenerSimple eventListener = new EventListenerSimple();
    public EventListenerBool gameActiveEventListener;

    public EventRaiserSimple playerHitRaiser = new EventRaiserSimple();
    public EventRaiserBool gameActiveEventRaiser;

    private bool isGameActive = true;
    private float gameEndTime;

    private void OnEnable()
    {
        eventListener.RegisterListener();
        eventListener.onEventRaisedAction = OnTargetHit;

        gameActiveEventListener.RegisterListener();
        gameActiveEventListener.onEventRaisedAction = GameStatusChanged;
    }

    private void OnDisable()
    {
        eventListener.UnregisterListener();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
            if (isGameActive)
            {
                GameObject bullet = bulletSpawner.GetPooledObject();
                bullet.SetActive(true);
                muzzleFlash.Play();
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);
                StartCoroutine(KillBullet(bullet));
            }
            else
            {
                if(Time.time - gameEndTime > 1)
                    gameActiveEventRaiser.RaiseEvent(true);
            }
        }
    }

    // <summary>
    /// Handle a bullet hitting an enemy target
    /// </summary>
    public void OnTargetHit()
    {
        currentScore.Increment();
    }

    // <summary>
    /// Kill a bullet after a delay to help with object pooling
    /// </summary>
    private IEnumerator KillBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.SetActive(false);
    }

    // <summary>
    /// Handles changes to the game status
    /// </summary>
    private void GameStatusChanged(bool isActive)
    {
        isGameActive = isActive;
        if (!isActive)
            gameEndTime = Time.time;
    }
}
