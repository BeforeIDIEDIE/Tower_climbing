using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMovement : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;
    public Transform player;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 3f;

    public float rotationSpeed = 5f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        StartCoroutine(ShootRoutine());
    }

    private void Update()
    {
        LookAtPlayer();
    }
    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (player != null && bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector3 direction = (player.position - firePoint.position).normalized;

            Destroy(bullet, 5f);
        }
    }
    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }
}