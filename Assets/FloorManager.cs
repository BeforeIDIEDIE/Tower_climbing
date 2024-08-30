using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public List<GameObject> enemySpawnPoints; 
    public GameObject enemyPrefab; 
    //public GameObject rewardPrefab;//맨 마지막 구현 
    public GameObject endPoint; 
    public Transform nextStagePosition; 

    private List<GameObject> enemies;

    private void Start()
    {
        enemies = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 적 생성
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        Debug.Log("생성시작");
        foreach (GameObject spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemies.Add(enemy);
        }
    }

    public void CheckEndConditions(GameObject player)
    {
        // 모든 적이 제거되었는지 확인
        bool allEnemiesDefeated = enemies.TrueForAll(enemy => enemy == null);

        if (allEnemiesDefeated)
        {
            // 보상 생성
            //Instantiate(rewardPrefab, endPoint.transform.position, Quaternion.identity);
            Debug.Log("Clear All");
        }
        else
        {
            // 플레이어를 다음 스테이지 위치로 이동
            MovePlayerToNextStage(player);
        }
    }

    private void MovePlayerToNextStage(GameObject player)
    {
        // 플레이어를 nextStagePosition으로 이동
        player.transform.position = nextStagePosition.position;
    }
}