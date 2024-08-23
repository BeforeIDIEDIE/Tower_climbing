using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 2f;
    private Vector3 fireDirection;

    public void Initialize(Vector3 direction)
    {
        fireDirection = direction.normalized;
    }

    void Update()
    {
        transform.position += fireDirection * speed * Time.deltaTime;
        transform.forward = fireDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Mathf.RoundToInt(damage));
            }
        }
        Destroy(gameObject);
    }
}
