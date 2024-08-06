using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public Transform turretBase;  // 포탑의 기본 부분
    public Transform turretHead;  // 포탑의 회전하는 상단 부분
    public Transform firePos;     // 총알이 발사되는 위치
    public float detectionRadius = 20f;
    public float rotationSpeed = 3f;
    public float fireRange = 15f; // 레이캐스트 거리
    public float fireInterval = 3f; // 발사 간격
    public string playerTag = "Player";
    public GameObject bulletPrefab; // 총알 프리팹

    private Transform player;
    private float lastFireTime;

    void Update()
    {
        DetectAndTrackPlayer();
        CheckAndFire();
    }

    void DetectAndTrackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(turretBase.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(playerTag))
            {
                player = hitCollider.transform;
                AimAtPlayer();
                return;
            }
        }
        player = null;
    }

    void AimAtPlayer()
    {
        if (player != null)
        {
            Vector3 targetDirection = player.position - turretHead.position;
            targetDirection.y = 0; // y축 회전은 무시
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void CheckAndFire()
    {
        if (player != null && Time.time - lastFireTime >= fireInterval)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, fireRange))
            {
                if (hit.collider.CompareTag(playerTag))
                {
                    Fire();
                    lastFireTime = Time.time;
                }
            }
        }
    }

    void Fire()
    {
        // 여기에 총알 발사 로직을 구현합니다.
        // 예를 들어, 총알 프리팹을 인스턴스화하고 발사할 수 있습니다.
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            // 필요한 경우 여기에 총알의 속도나 기타 속성을 설정합니다.
        }
        Debug.Log("Fire!"); // 디버깅용 로그
    }

    void OnDrawGizmosSelected()
    {
        if (turretBase != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(turretBase.position, detectionRadius);
        }

        if (firePos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(firePos.position, firePos.forward * fireRange);
        }
    }
}
