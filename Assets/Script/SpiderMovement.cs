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
    private bool isGrounded = false;

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
            yield return new WaitUntil(() => isGrounded);
            yield return new WaitForSeconds(jumpDelay);
            Jump();
            yield return new WaitUntil(() => !isGrounded);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("����!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("���� ������");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("������ ���");
        }
    }
}
