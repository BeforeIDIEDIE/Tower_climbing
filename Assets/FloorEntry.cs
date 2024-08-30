using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEntry : MonoBehaviour
{
    [SerializeField]public FloorManager floorManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            floorManager.SpawnEnemies();
        }
    }
}
