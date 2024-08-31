using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troopMovement : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 1f;
    public float rotationSpeed = 5f;
    public Transform forward;
    private bool movingToEnd = true;
    private Transform currentTarget;

    void Start()
    {
        currentTarget = point2;
    }

    void Update()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (forward != null)
        {
            forward.localRotation = Quaternion.identity;
        }

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (movingToEnd)
            {
                currentTarget = point1;
                movingToEnd = false;
            }
            else
            {
                currentTarget = point2;
                movingToEnd = true;
            }
        }
    }
}
