using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    public FloorManager floorManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // EndPoint�� �÷��̾ �������� �� FloorManager�� CheckEndConditions �޼��� ȣ��
            if (floorManager != null)
            {
                floorManager.CheckEndConditions(other.gameObject);
            }
        }
    }
}