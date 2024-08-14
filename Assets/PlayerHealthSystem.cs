using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private int currentHealth;
    private Rigidbody rb;
    private bool canTakeDamage = true;
    private float invincibleTime = 2f;

    // ��Ʈ �̹����� ���� �ʵ� �߰�
    [SerializeField] private Image[] healthImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        UpdateHealthUI();
    }

    private void Update()
    {
        CheckForDamage();
    }

    private void CheckForDamage()
    {
        if (canTakeDamage)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("EnemyBall"))
                {
                    TakeDamage(1);
                    Destroy(collider.gameObject);
                    StartCoroutine(DamageCooldown());
                    break;
                }
                else if (collider.CompareTag("Enemy"))
                {
                    TakeDamage(1);
                    StartCoroutine(DamageCooldown());
                    break;
                }
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibleTime);
        canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (currentHealth == 1)
        {
            StartCoroutine(LowHealthEffect());
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
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].sprite = fullHeart;
            }
            else
            {
                healthImages[i].sprite = emptyHeart;
            }
        }
        Debug.Log($"���� ü��: {currentHealth}/{maxHealth}");
    }

    private IEnumerator LowHealthEffect()
    {
        Color originalColor = Color.white;
        Color targetColor = Color.red;

        while (currentHealth == 1)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 3)
            {
                Color currentColor = Color.Lerp(originalColor, targetColor, t);
                foreach (Image img in healthImages)
                {
                    img.color = currentColor;
                }
                yield return null;
            }

            for (float t = 0; t < 1; t += Time.deltaTime / 3)
            {
                Color currentColor = Color.Lerp(targetColor, originalColor, t);
                foreach (Image img in healthImages)
                {
                    img.color = currentColor;
                }
                yield return null;
            }
        }

        foreach (Image img in healthImages)
        {
            img.color = originalColor;
        }
    }

    public int CurrentHealth => currentHealth;

    public int MaxHealth => maxHealth;
}