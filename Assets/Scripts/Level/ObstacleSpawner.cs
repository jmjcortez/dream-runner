using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] obstacle;

    private Vector3 spawnObstaclePosition;

    void Update() {
        float distanceToPlayer = Vector2.Distance(player.gameObject.transform.position, spawnObstaclePosition);

        if (distanceToPlayer < 120) {
            SpawnObstacle();
        }
    }

    void SpawnObstacle() {
        spawnObstaclePosition = new Vector2(spawnObstaclePosition.x + 30 , -2.51f);

        Instantiate(obstacle[0], spawnObstaclePosition, Quaternion.identity);
    }
}
