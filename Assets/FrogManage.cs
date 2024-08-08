using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManage : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpInterval = 2f;
    public float jumpAngle = 45f; // 점프 각도
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
        // 속도 벡터 생성
        jumpVelocity = new Vector3(vx, vy, vz);
        rb.AddForce(jumpVelocity, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);//충돌지점에서 법선벡터를 이용해 반사각을 구함-> 반사벡터
            transform.Rotate(transform.behind());
            rb.velocity = reflectedVelocity;
        }
    }
}
