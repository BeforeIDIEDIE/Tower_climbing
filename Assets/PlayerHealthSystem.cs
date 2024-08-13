using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private int currentHealth;
    private Rigidbody rb;
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        UpdateHealthUI();
    }

    private void Die()
    {
        Debug.Log("플레이어가 사망했습니다.");
        if (rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
        }
    }

    private void UpdateHealthUI()
    {
        // 여기에 UI 업데이트 로직 구현
        Debug.Log($"현재 체력: {currentHealth}/{maxHealth}");
    }

    // 현재 체력 확인용 프로퍼티
    public int CurrentHealth => currentHealth;

    // 최대 체력 확인용 프로퍼티
    public int MaxHealth => maxHealth;
}
