using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatManager : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float rotationSpeed = 2f;
    public float moveSpeed = 3f;
    public string playerTag = "Player";

    private Transform player;
    private bool isChasing = false;

    void Update()
    {
        if (!isChasing)
        {
            DetectPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);


        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(playerTag))
            {
                player = hitCollider.transform;
                isChasing = true;
                return;
            }
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;//������ ����, ����ȭ

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, player.position) > detectionRadius)//�÷��̾� �������� ���?
            {
                isChasing = false;
                player = null;
            }
        }
        else
        {
            isChasing = false;
        }
    }

    void OnDrawGizmosSelected()//������
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
