using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //1인칭 카메라!
    public float lookSpeed = 2f;
    public float lookXLimit = 80f;
    private float rotationX = 0f;
    
    
    private Camera playerCamera;
    private Rigidbody rb;

    //이동 관련
    private bool isGrounded = true; 
    public float jumpForce = 5f;
    private float moveSpeed = 5f;

    public float getMoveSpeed => moveSpeed;
    public float getBulletSpeed => bulletSpeed;
    public void setMoveSpeed(float gob)
    {
        moveSpeed *= gob; 
    }
    public void setBulletSpeed(float gob)
    {
        bulletSpeed *= gob;
    }
    public void setNextFireTime(float gob)
    {
        nextFireTime *= gob;
    }
    //총알 관련
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    private float nextFireTime = 3f;


    void Start()
    {
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //카메라 회전 부
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * lookSpeed, 0f);

        //사용자 이동 부
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = transform.TransformDirection(movement);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        //점프 부
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        AimCheck();

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        nextFireTime = Time.time + fireRate;
        
        Vector3 firePos = playerCamera.transform.position + playerCamera.transform.forward;
        
        GameObject bullet = Instantiate(bulletPrefab, firePos, Quaternion.identity);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        
        if (bulletScript != null)
        {
            bulletScript.Initialize(playerCamera.transform.forward);
            bulletScript.speed = bulletSpeed;
        }

        Destroy(bullet, 3f);
    }
    //void FireBullet()
    //{
    //    nextFireTime = Time.time + fireRate;

    //    GameObject bullet = Instantiate(bulletPrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);


    //    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
    //    if (bulletRb != null)
    //    {
    //        bulletRb.velocity = playerCamera.transform.forward * bulletSpeed;
    //    }

    //    Destroy(bullet, 3f);
    //}

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void AimCheck()
    {
        Transform CamTransform = playerCamera.transform;
    }
}
