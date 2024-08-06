using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public Transform turretBase;  // ��ž�� �⺻ �κ�
    public Transform turretHead;  // ��ž�� ȸ���ϴ� ��� �κ�
    public Transform firePos;     // �Ѿ��� �߻�Ǵ� ��ġ
    public float detectionRadius = 20f;
    public float rotationSpeed = 3f;
    public float fireRange = 15f; // ����ĳ��Ʈ �Ÿ�
    public float fireInterval = 3f; // �߻� ����
    public string playerTag = "Player";
    public GameObject bulletPrefab; // �Ѿ� ������

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
            targetDirection.y = 0; // y�� ȸ���� ����
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
        // ���⿡ �Ѿ� �߻� ������ �����մϴ�.
        // ���� ���, �Ѿ� �������� �ν��Ͻ�ȭ�ϰ� �߻��� �� �ֽ��ϴ�.
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            // �ʿ��� ��� ���⿡ �Ѿ��� �ӵ��� ��Ÿ �Ӽ��� �����մϴ�.
        }
        Debug.Log("Fire!"); // ������ �α�
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
