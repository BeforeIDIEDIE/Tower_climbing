using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float detectionRange = 2f;
    public float jumpForce = 3f;
    private bool isAttached = true;
    private Rigidbody rb;
    public Transform[] leftLegs;
    public Transform[] rightLegs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (isAttached)
        {
            CheckPlayerBelow();
        }
        else
        {
            StartCoroutine(Jump());
        }
    }

    void CheckPlayerBelow()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position; 

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("ÀÎ½Ä!");
                isAttached = false;
                rb.useGravity = true;
            }
        }
        Debug.DrawLine(rayOrigin, rayOrigin + Vector3.down * detectionRange, Color.red, 0.1f);
    }

    IEnumerator Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(2f);
    }
}
