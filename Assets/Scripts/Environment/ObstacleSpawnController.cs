using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnController : MonoBehaviour
{   
    public Transform[] obstaclePoints;
    public GameObject[] obstacleChoices, obstacles; 
    
    public float chanceToSpawn;

    public bool inFirstBuilding;

    private void Start() {
        SpawnObstacles();
    }

    public void SpawnObstacles() {
        DestroyObstacles();

        for (int i = 0; i < obstaclePoints.Length; i++) {

            float spawnSelect = Random.Range(0, 100f);

            if (inFirstBuilding && i ==0) {
                continue;
            }

            if (spawnSelect <= chanceToSpawn) {
                Instantiate(obstacleChoices[0], obstaclePoints[i].transform.position, obstaclePoints[i].transform.rotation);
            }
        }
    }

    public void DestroyObstacles() {
        for (int i = 0; i < obstacles.Length; i++) {
            Destroy(obstacles[i]);
        }
    }
}
