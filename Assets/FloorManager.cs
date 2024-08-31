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
    public GameObject endPoint;
    public Transform nextStagePosition;

    private List<GameObject> enemies;
    private int enemiesDefeated = 0;

    private void Start()
    {
        enemies = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        Debug.Log("생성시작");
        foreach (EnemySpawnInfo spawnInfo in enemySpawnInfos)
        {
            GameObject enemyPrefab = (spawnInfo.enemyType == EnemyType.Enemy1) ? enemyPrefab1 : enemyPrefab2;
            GameObject enemyObject = Instantiate(enemyPrefab,
                                           spawnInfo.spawnPoint.transform.position,
                                           spawnInfo.spawnPoint.transform.rotation);
            enemies.Add(enemyObject);

            EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.OnEnemyDeath += OnEnemyDefeated;
            }
        }
    }

    private void OnEnemyDefeated(EnemyHealth enemy)
    {
        enemiesDefeated++;
        Debug.Log($"Enemy defeated! Total enemies defeated: {enemiesDefeated}");
    }

    public void CheckEndConditions(GameObject player)
    {
        int totalEnemies = enemySpawnInfos.Count;
        Debug.Log($"Defeated enemies: {enemiesDefeated} / Total enemies: {totalEnemies}");

        bool allEnemiesDefeated = (enemiesDefeated == totalEnemies);
        if (allEnemiesDefeated)
        {
            Debug.Log($"Clear All. Total enemies defeated: {enemiesDefeated}");
        }
        else
        {
            Debug.Log($"Not all enemies defeated yet. Enemies defeated: {enemiesDefeated}");
            MovePlayerToNextStage(player);
        }
    }

    private void MovePlayerToNextStage(GameObject player)
    {
        DestroyAllEnemies();
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
        enemiesDefeated = 0;
    }
}