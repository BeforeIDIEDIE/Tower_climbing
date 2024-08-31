using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    public FloorManager floorManager;
    public GameManager GM;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (floorManager != null)
            {
                GM.StartFadeOutEffect();
                floorManager.CheckEndConditions(other.gameObject);

            }
        }
    }
}
