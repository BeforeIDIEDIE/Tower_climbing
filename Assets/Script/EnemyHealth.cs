using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyHealth : MonoBehaviour
{
    public bool isDead = false;
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;

    public event Action<EnemyHealth> OnEnemyDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("ÇÇÇØ" + damage);
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }
}