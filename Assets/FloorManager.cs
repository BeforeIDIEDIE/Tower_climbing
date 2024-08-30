using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public List<GameObject> enemySpawnPoints; 
    public GameObject enemyPrefab; 
    //public GameObject rewardPrefab;//�� ������ ���� 
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
            // �� ����
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        Debug.Log("��������");
        foreach (GameObject spawnPoint in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemies.Add(enemy);
        }
    }

    public void CheckEndConditions(GameObject player)
    {
        // ��� ���� ���ŵǾ����� Ȯ��
        bool allEnemiesDefeated = enemies.TrueForAll(enemy => enemy == null);

        if (allEnemiesDefeated)
        {
            // ���� ����
            //Instantiate(rewardPrefab, endPoint.transform.position, Quaternion.identity);
            Debug.Log("Clear All");
        }
        else
        {
            // �÷��̾ ���� �������� ��ġ�� �̵�
            MovePlayerToNextStage(player);
        }
    }

    private void MovePlayerToNextStage(GameObject player)
    {
        // �÷��̾ nextStagePosition���� �̵�
        player.transform.position = nextStagePosition.position;
    }
}