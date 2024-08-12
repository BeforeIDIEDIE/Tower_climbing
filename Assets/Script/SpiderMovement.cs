using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpiderMovement : MonoBehaviour
{
    public float jumpForce = 1.5f;
    public float jumpDelay = 3f;
    public GameObject detector;
    private bool isTriggered = false;
    private Rigidbody rb;
    public float groundCheckDistance = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void TriggerMovement()
    {
        if (!isTriggered)
        {
            Debug.Log("플레이어가 감지되었습니다!");
            isTriggered = true;
            rb.useGravity = true;
            rb.isKinematic = false;
            StartCoroutine(JumpRoutine());
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsGrounded());
            yield return new WaitForSeconds(jumpDelay);
            Jump();
            yield return new WaitUntil(() => !IsGrounded());
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("점프!");
        }
    }

    bool IsGrounded()
    {
        // 스파이더의 위치에서 아래로 레이캐스트 발사
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("땅 확인됨");
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("땅에 충돌");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("땅에서 벗어남");
        }
    }
}
