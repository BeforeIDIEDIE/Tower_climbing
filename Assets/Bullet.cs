using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 2f;
    private Vector3 fireDirection;
    private bool hasCollided = false;

    public void Initialize(Vector3 direction)
    {
        fireDirection = direction.normalized;
    }

    void Update()
    {
        transform.position += fireDirection * speed * Time.deltaTime;
        transform.forward = fireDirection;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided)
        {
            return;
        }

        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasCollided = true;
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
