using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private Vector2 startPosition;

    private int distanceTraveled;

    public static LevelManager instance;

    public GameObject player;
    public TextMeshProUGUI odometerDisplay;

    private int lives = 0;

    private void Awake() {
        instance = this;
    }
    
    void Start()
    {
        startPosition = player.transform.position;
    }

    void Update()
    {
        distanceTraveled = (int)Vector2.Distance(startPosition, player.transform.position);
    
        UpdateOdometer();
    }

    void UpdateOdometer() {
        odometerDisplay.text = distanceTraveled.ToString();
    }

    public void HandleGameOver() {
        if (lives < 1) {
            Player.instance.playerRigidbody.velocity = new Vector2(0, Player.instance.playerRigidbody.velocity.y);
            Player.instance.isFrozen = true;

            CameraController.instance.isFollowingTarget = false;
        } 
    }
}