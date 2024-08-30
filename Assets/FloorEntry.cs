using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEntry : MonoBehaviour
{
    private bool used = false;
    [SerializeField]public FloorManager floorManager;
    private void OnTriggerEnter(Collider other)
    {
        if(used)
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            floorManager.SpawnEnemies();
            used = true ;
        }
    }
}
