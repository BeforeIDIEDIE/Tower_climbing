using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Serializable]
    public struct EnemySpawnInfo
    {
        public GameObject spawnPoint;
        public EnemyType enemyType;
    }

    public enum EnemyType
    {
        Enemy1,
        Enemy2
    }

    public List<EnemySpawnInfo> enemySpawnInfos;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
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
        foreach (EnemySpawnInfo spawnInfo in enemySpawnInfos)
        {
            GameObject enemyPrefab = (spawnInfo.enemyType == EnemyType.Enemy1) ? enemyPrefab1 : enemyPrefab2;
            GameObject enemy = Instantiate(enemyPrefab,
                                           spawnInfo.spawnPoint.transform.position,
                                           spawnInfo.spawnPoint.transform.rotation);
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
        DestroyAllEnemies();
        // 플레이어를 nextStagePosition으로 이동
        player.transform.position = nextStagePosition.position;
    }

    private void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        enemies.Clear();
    }
}