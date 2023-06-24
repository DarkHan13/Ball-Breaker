using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10f; // Speed at which the bullet moves
    public float lifetime = 2f; // Time until the bullet is destroyed
    private Vector3 _dir;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
        _dir = new Vector3(1, Random.Range(-0.02f, 0.05f));
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(_dir * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<ZobmieMovement>();
            enemy.GetDamage(5f, transform.position, startPos);
            Destroy(gameObject);
        } else if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
