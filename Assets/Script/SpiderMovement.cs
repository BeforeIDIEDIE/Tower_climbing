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
            Debug.Log("�÷��̾ �����Ǿ����ϴ�!");
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
            Debug.Log("����!");
        }
    }

    bool IsGrounded()
    {
        // �����̴��� ��ġ���� �Ʒ��� ����ĳ��Ʈ �߻�
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("�� Ȯ�ε�");
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("���� �浹");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("������ ���");
        }
    }
}
