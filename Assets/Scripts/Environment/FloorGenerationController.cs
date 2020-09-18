using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerationController : MonoBehaviour    
{   

    public SpriteRenderer floorSpriteRenderer;

    public Transform spawnPoint;

    public GameObject obstacleManager, tabManager;

    private float floorLength, spawnTime;

    private bool isSpawning;

    private void Start() {
        floorLength = floorSpriteRenderer.bounds.size.x;
    }

    private void Update() {
        if (isSpawning && !Player.instance.isFrozen) {
            spawnTime -= Time.deltaTime;

            if (spawnTime < 0) {
                MoveFloor();
                isSpawning = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        spawnTime = 3;
        isSpawning = true;
    }

    void MoveFloor() {
        gameObject.transform.parent.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);

        obstacleManager.GetComponent<ObstacleSpawnController>().SpawnObstacles();
        tabManager.GetComponent<TabGenerator>().SpawnTab();

    }

}
