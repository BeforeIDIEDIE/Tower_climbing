
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBall : MonoBehaviour
{
    public float speed = 4f;
    public float rotateSpeed = 100f;
    public float lifetime = 10f;
    public Transform target;
    private Rigidbody rb;
    public Transform dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }


        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        dir.rotation = Quaternion.Slerp(dir.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
        Vector3 moveDirection = dir.forward;
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        transform.rotation = dir.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BossBall collided with: " + collision.gameObject.name);
    }
}