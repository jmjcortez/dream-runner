using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerationController : MonoBehaviour    
{   

    public SpriteRenderer floorSpriteRenderer;

    public Transform spawnPoint;

    public GameObject obstacleManager;

    private float floorLength;

    private void Start() {
        floorLength = floorSpriteRenderer.bounds.size.x;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(MoveFloor()); 
    }

    IEnumerator MoveFloor() {
        yield return new WaitForSeconds(3);

        // gameObject.transform.parent.position = new Vector2(gameObject.transform.parent.position.x + (floorLength * 2) + Random.Range(4,7), gameObject.transform.parent.position.y);
        gameObject.transform.parent.position = new Vector2(spawnPoint.position.x + Random.Range(0,2), spawnPoint.position.y);

        obstacleManager.GetComponent<ObstacleSpawnController>().SpawnObstacles();

    }

}
