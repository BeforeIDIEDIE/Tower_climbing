using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManage : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpInterval = 2f;
    public float jumpAngle = 45f; // ���� ����
    private Rigidbody rb;
    private float nextJumpTime;
    private Vector3 jumpVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextJumpTime = Time.time + jumpInterval;
    }

    void Update()
    {
        if (Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval;
        }
    }

    void Jump()
    {
        float angle = jumpAngle * Mathf.Deg2Rad;
        float vx = jumpForce * transform.forward.x * Mathf.Cos(angle);
        float vz = jumpForce * transform.forward.z * Mathf.Cos(angle);
        float vy = jumpForce * Mathf.Sin(angle);
        // �ӵ� ���� ����
        jumpVelocity = new Vector3(vx, vy, vz);
        rb.AddForce(jumpVelocity, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);//�浹�������� �������͸� �̿��� �ݻ簢�� ����-> �ݻ纤��
            transform.Rotate(transform.behind());
            rb.velocity = reflectedVelocity;
        }
    }
}
