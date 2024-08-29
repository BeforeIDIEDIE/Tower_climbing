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
    private bool SpikeImmune = false;
    private float invincibleTime = 2f;
    
    // 하트 이미지
    [SerializeField] private Image[] healthImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        UpdateHealthUI();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canTakeDamage)
        {
            if (collision.gameObject.CompareTag("EnemyBall"))
            {
                TakeDamage(1);
                Destroy(collision.gameObject);
                StartCoroutine(DamageCooldown());
            }
            else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("SpikeBall"))
            {
                TakeDamage(1);
                StartCoroutine(DamageCooldown());
            }
            else if (collision.gameObject.CompareTag("Spike") && !SpikeImmune)
            {
                TakeDamage(1);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibleTime);
        canTakeDamage = true;
    }
    private void UpdateHealthUI()
    {
        for (int i = 0; i < maxHealth; i++)
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
        Debug.Log($"현재 체력: {currentHealth}/{maxHealth}");
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

    public void IncreaseMaxHealth()
    {
        maxHealth += 1;
        if (maxHealth <= healthImages.Length)
        {
            healthImages[maxHealth - 1].gameObject.SetActive(true);
        }
        UpdateHealthUI();
    }
    public void InvincibleTimeUp()
    {
        invincibleTime++;
    }

    public void SpikeImmuneOn()
    {
        SpikeImmune = true;
    }
    private void Die()
    {
        Debug.Log("플레이어가 사망했습니다.");
        if (rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
        }
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
