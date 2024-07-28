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
        playerTransform = GetComponent<Transform>();// Rigidbody의 회전을 고정하여 물리 연산에 영향을 주지 않도록 설정
        playerRigidbody.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // [변경] 카메라 기반 이동을 위해 카메라의 방향 벡터를 가져옵니다.
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // [변경] y축 성분을 0으로 만들어 수평 이동만 처리합니다.
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // [변경] 카메라 방향을 기준으로 이동 방향을 계산합니다.
        Vector3 moveDirection = forward * inputY + right * inputX;

        // [변경] 이동 방향을 정규화하고 속도를 적용합니다.
        moveDirection.Normalize();
        Vector3 movement = moveDirection * speed;//playerRigidbody.AddForce(new Vector3(inputX * speed, 0, inputY * speed));//이 방법은 관성이 붙기에 조작이 쉽지 않음.        

        // [변경] 수직 속도는 유지합니다.
        movement.y = playerRigidbody.velocity.y;

        // [변경] 최종 속도를 적용합니다.
        playerRigidbody.velocity = movement;

        // [추가] 회전 처리 메서드를 호출합니다.
        Rotate();

        // 점프 로직 (기존과 동일)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 리셋 로직 (기존과 동일)
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerTransform.position = new Vector3(0, 1.84f, -50);
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // 마우스 X축 입력에 따라 수평 회전 값을 조정
        xRotation -= mouseY;    // 마우스 Y축 입력에 따라 수직 회전 값을 조정

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 수직 회전 값을 -90도에서 90도 사이로 제한

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // 카메라의 회전을 조절
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // 플레이어 캐릭터의 회전을 조절
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
