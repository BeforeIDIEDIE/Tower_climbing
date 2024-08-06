using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private SpiderMovement spiderMovement;

    void Start()
    {
        spiderMovement = GetComponentInParent<SpiderMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spiderMovement.TriggerMovement();
        }
    }
}
