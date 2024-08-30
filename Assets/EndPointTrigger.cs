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
            // EndPoint에 플레이어가 도달했을 때 FloorManager의 CheckEndConditions 메서드 호출
            if (floorManager != null)
            {
                GM.StartFadeOutEffect();
                floorManager.CheckEndConditions(other.gameObject);

            }
        }
    }
}
