using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Used for controlling the behaviour of Enemy objects
/// </summary>
public class Enemy : MonoBehaviour
{
    public EventRaiserSimple targetHitRaiser;
    public float speed = 1f;
    public EventRaiserSimple playerHitRaiser = new EventRaiserSimple();
    public TagID playerTagId;

    public ParticleSystem explosion;
    public MeshRenderer sphereMesh;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        targetHitRaiser.RaiseEvent();
        StartCoroutine(KillEnemy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsColliderValid(collision.collider))
        {
            StartCoroutine(KillEnemy());
            playerHitRaiser.RaiseEvent();
        }
    }

    private IEnumerator KillEnemy()
    {
        sphereMesh.enabled = false;
        explosion.Play();
        yield return new WaitForSeconds(2);
        sphereMesh.enabled = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            target.position *= -1.0f;
        }
    }

    // <summary>
    /// Tests is the colliding object has the appropriate tag
    /// </summary>
    public bool IsColliderValid(Collider collider)
    {
        var tag = collider.GetComponent<Tag>();
        if (tag == null)
        {
            return false;
        }

        if (tag.HasTagID(playerTagId))
        {
            return true;
        }

        return false;
    }
}
