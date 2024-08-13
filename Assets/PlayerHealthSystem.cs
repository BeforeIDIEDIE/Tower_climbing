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
        Debug.Log("�÷��̾ ����߽��ϴ�.");
        if (rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
        }
    }

    private void UpdateHealthUI()
    {
        // ���⿡ UI ������Ʈ ���� ����
        Debug.Log($"���� ü��: {currentHealth}/{maxHealth}");
    }

    // ���� ü�� Ȯ�ο� ������Ƽ
    public int CurrentHealth => currentHealth;

    // �ִ� ü�� Ȯ�ο� ������Ƽ
    public int MaxHealth => maxHealth;
}
