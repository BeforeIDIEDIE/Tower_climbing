using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlayer : MonoBehaviour
{
    private float speed = 3f;
    private float jumpForce = 5f;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private bool isGrounded = false;
    public float mouseSpeed;
    public Camera cam;

    float yRotation;
    float xRotation;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();// Rigidbody�� ȸ���� �����Ͽ� ���� ���꿡 ������ ���� �ʵ��� ����
        playerRigidbody.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // [����] ī�޶� ��� �̵��� ���� ī�޶��� ���� ���͸� �����ɴϴ�.
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // [����] y�� ������ 0���� ����� ���� �̵��� ó���մϴ�.
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // [����] ī�޶� ������ �������� �̵� ������ ����մϴ�.
        Vector3 moveDirection = forward * inputY + right * inputX;

        // [����] �̵� ������ ����ȭ�ϰ� �ӵ��� �����մϴ�.
        moveDirection.Normalize();
        Vector3 movement = moveDirection * speed;//playerRigidbody.AddForce(new Vector3(inputX * speed, 0, inputY * speed));//�� ����� ������ �ٱ⿡ ������ ���� ����.        

        // [����] ���� �ӵ��� �����մϴ�.
        movement.y = playerRigidbody.velocity.y;

        // [����] ���� �ӵ��� �����մϴ�.
        playerRigidbody.velocity = movement;

        // [�߰�] ȸ�� ó�� �޼��带 ȣ���մϴ�.
        Rotate();

        // ���� ���� (������ ����)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // ���� ���� (������ ����)
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerTransform.position = new Vector3(0, 1.84f, -50);
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // ���콺 X�� �Է¿� ���� ���� ȸ�� ���� ����
        xRotation -= mouseY;    // ���콺 Y�� �Է¿� ���� ���� ȸ�� ���� ����

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ȸ�� ���� -90������ 90�� ���̷� ����

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // �÷��̾� ĳ������ ȸ���� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
