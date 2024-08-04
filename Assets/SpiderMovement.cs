using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float detectionRange = 2f;
    public float jumpForce = 3f;
    public float preparationTime = 0.2f;
    private bool isAttached = true;
    private Rigidbody rb;
    private Vector3 initialPosition;
    public Transform[] leftLegs;
    public Transform[] rightLegs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isAttached)
        {
            CheckPlayerBelow();
        }
        else if (transform.position.y >= initialPosition.y)
        {
            ResetPosition();
        }
    }

    void CheckPlayerBelow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                StartCoroutine(DetachAndJump());
            }
        }
    }

    IEnumerator DetachAndJump()
    {
        isAttached = false;
        rb.useGravity = true; 

        yield return new WaitUntil(() => Physics.Raycast(transform.position, Vector3.down, 0.1f));

        while (true)
        {
            SetLegAngles(leftLegs, 0f);
            SetLegAngles(rightLegs, 75f);
            yield return new WaitForSeconds(preparationTime);

            SetLegAngles(leftLegs, 90f);
            SetLegAngles(rightLegs, 90f);
            rb.velocity = Vector3.zero; 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            yield return new WaitUntil(() => rb.velocity.y <= 0);
            yield return new WaitUntil(() => Physics.Raycast(transform.position, Vector3.down, 0.1f));
        }
    }

    void SetLegAngles(Transform[] legs, float angle)
    {
        foreach (Transform leg in legs)
        {
            leg.localRotation = Quaternion.Euler(-60, angle, -90);
        }
    }

    void ResetPosition()
    {
        StopAllCoroutines();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = initialPosition;
        isAttached = true;
    }
}
