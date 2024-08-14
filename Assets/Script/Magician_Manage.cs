using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician_Manage : MonoBehaviour
{
    public float detectionRadius = 15f;
    public float fireInterval = 5f;
    public GameObject bulletPrefab;
    public Transform firePos;
    public Transform player;
    private float nextFireTime;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && firePos != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Homing_Shot homingShot = bullet.GetComponent<Homing_Shot>();
            if (homingShot != null)
            {
                homingShot.target = player; 
            }
        }
    }
}
