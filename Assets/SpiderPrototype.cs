using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPrototype : MonoBehaviour
{
    public float raycastDistance = 3f;
    public float jumpForce = 7f;
    public float jumpDelay = 10f; 
    public Transform detector1;
    public Transform detector2;
    public Transform detector3;
    private bool isTriggered = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (!isTriggered)
        {
            DetectPlayer(detector1);
            DetectPlayer(detector2);
            DetectPlayer(detector3);
        }
    }

    void DetectPlayer(Transform detector)
    {
        RaycastHit hit;
        if (Physics.Raycast(detector.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("플레이어가 감지되었습니다!");
                isTriggered = true;
                rb.useGravity = true;
                rb.isKinematic = false;
                StartCoroutine(JumpRoutine());
            }
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => isGrounded);
            Debug.Log("착지 확인");

            // 점프 딜레이
            Debug.Log("점프 딜레이 시작");
            yield return new WaitForSeconds(jumpDelay);
            Debug.Log("점프 딜레이 종료");

            // 점프 실행
            Jump();

            // 공중에 있는 동안 대기
            yield return new WaitUntil(() => !isGrounded);
            yield return new WaitUntil(() => isGrounded);
        }
    }

    void Jump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}