using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minDistanceFromOrigin = 10f;
    public float fixedY = 1f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 2.5f);
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = GetRandomPosition();

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;

        do
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);

            randomPosition = new Vector3(x, fixedY, z);
        }while (Vector3.Distance(randomPosition, Vector3.zero) < minDistanceFromOrigin);

        return randomPosition;
    }
}
