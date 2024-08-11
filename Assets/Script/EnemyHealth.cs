using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)//���� �Ѿ��̶� �ε��� �� �Լ� ����
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //��ƼŬ ȿ��, �ı��ɶ� �Ҹ� ���� ����
        Destroy(gameObject);
    }
}
