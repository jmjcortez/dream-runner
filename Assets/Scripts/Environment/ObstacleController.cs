using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !Player.instance.isInvulnerable && !Player.instance.isFrozen) {
            StartCoroutine(LevelManager.instance.HandleGameOver(0));
        }
    }

}
