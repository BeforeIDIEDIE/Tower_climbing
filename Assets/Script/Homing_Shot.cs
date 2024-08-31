using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Shot : MonoBehaviour
{
    public float speed = 3f;
    public float rotateSpeed = 100f;
    public float lifetime = 5f;  

    public Transform target;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);  
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;

        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.fixedDeltaTime));
        
        
        rb.velocity = transform.forward * speed;
    }
}
