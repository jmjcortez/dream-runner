using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnController : MonoBehaviour
{   
    public Transform[] obstaclePoints, tabSpawnPoints;
    public GameObject[] obstacleChoices, obstacles; 

    public GameObject tab;
    
    public float chanceToSpawn;

    public bool inFirstBuilding;

    private void Start() {
        obstacles = new GameObject[3];

        SpawnObstacles();

        inFirstBuilding = false;
    }

    private void Update() {
        if (LevelManager.instance.distanceTraveled % 50 == 0 && LevelManager.instance.distanceTraveled > 0 && chanceToSpawn < 75) {
            chanceToSpawn += 0.2f;
        }
    }

    public void SpawnObstacles() {
        DestroyObstacles();

        for (int i = 0; i < obstaclePoints.Length; i++) {

            float spawnSelect = Random.Range(0, 100f);

            if (inFirstBuilding && i == 0) {
                continue;
            }

            if (spawnSelect <= chanceToSpawn) {
                obstacles[i] = Instantiate(obstacleChoices[0], obstaclePoints[i].transform.position, obstaclePoints[i].transform.rotation);
            }
        }
    }

    public void DestroyObstacles() {

        for (int i = 0; i < obstacles.Length; i++) {
            Destroy(obstacles[i]);
        }
    }

}
