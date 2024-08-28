using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCollision : MonoBehaviour
{
    public GameManager gameManager; 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(gameManager.GameEnter());
        }
    }
}
