using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMovement : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private int moveSpeed = 5;
    public Slider healthBar;
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 5f;
    public float rotationSpeed = 5f;
    public Material transparentMaterial;
    private Rigidbody rb;
    private Dictionary<Renderer, Material> originalMaterials = new Dictionary<Renderer, Material>();//c++맵같은 기능 -> 키: 값 대응
    private float nextFireTime;
    private bool isCloaking = false;
    public GameManager gm;
    public float cloakDistance = 5f;
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        GetChildRenderers(transform);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        LookAtPlayer();
        if (currentHealth >= 6)
        {
            CheckAndShoot();
        }
        else if(currentHealth==5)
        {
            rb.useGravity = true;
            CloakAndChase();
        }
        else
        {
            CloakAndChase();
        }
    }

    private void CloakAndChase()
    {
        if (!isCloaking)
        {
            isCloaking = true;
            StartCoroutine(BlinkAndSetMaterial(transparentMaterial));
        }
        
        ChasePlayer();
        //쫒는 기능
    }
    void ChasePlayer()
    {
        if (player != null && firePoint != null)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            firePoint.rotation = Quaternion.Slerp(firePoint.rotation, lookRotation, Time.deltaTime * rotationSpeed);


            Vector3 movement = Vector3.MoveTowards(rb.position, player.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(movement);

            float distanceToPlayer = Vector3.Distance(firePoint.position, player.position);
            if (distanceToPlayer <= cloakDistance)
            {
                RestoreOriginalMaterials();
            }
        }
    }
    void GetChildRenderers(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name != "Point" && child.name != "eye")
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer.material != null)
                {
                    originalMaterials[renderer] = renderer.material;
                }
                GetChildRenderers(child);
            }
        }
    }

    private void CheckAndShoot()
    {
        if (Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Homing_Shot homingShot = bullet.GetComponent<Homing_Shot>();
        if (homingShot != null)
        {
            homingShot.target = player;
        }
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        Debug.Log("현재 체력" + currentHealth);
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
        gm.BossDied();
        Destroy(gameObject);
    }

    IEnumerator BlinkAndSetMaterial(Material targetMaterial)
    {
        int blinkCount = 2;
        float blinkDuration = 0.1f;

        for (int i = 0; i < blinkCount; i++)
        {
            SetMaterialForAllRenderers(transparentMaterial);
            yield return new WaitForSeconds(blinkDuration);
            RestoreOriginalMaterials();
            yield return new WaitForSeconds(blinkDuration);
        }

        SetMaterialForAllRenderers(targetMaterial);
    }

    void SetMaterialForAllRenderers(Material material)
    {
        foreach (var kvp in originalMaterials)
        {
            if (kvp.Key != null)
            {
                kvp.Key.material = material;
            }
        }
    }

    void RestoreOriginalMaterials()
    {
        foreach (var kvp in originalMaterials)
        {
            if (kvp.Key != null)
            {
                kvp.Key.material = kvp.Value;
            }
        }
    }

    
}