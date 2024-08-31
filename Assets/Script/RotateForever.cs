using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForever : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;  // 회전 속도
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatAmplitude = 1f;//상하 이동반경

    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);


        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
