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

    private int lives = 1;

    private void Awake() {
        instance = this;
    }
    
    void Start()
    {
        startPosition = player.transform.position;
    }

    void Update()
    {
        if (!Player.instance.isFrozen) {
            distanceTraveled = (int)Vector2.Distance(startPosition, player.transform.position);
    
            UpdateOdometer();
        }
        
    }

    void UpdateOdometer() {
        odometerDisplay.text = distanceTraveled.ToString();
    }

    public IEnumerator HandleGameOver(int waitTime) {

            if (lives > 0) {
                yield return new WaitForSeconds(waitTime);

                Player.instance.playerRigidbody.velocity = new Vector2(0, Player.instance.playerRigidbody.velocity.y);
                Player.instance.isFrozen = true;
                Player.instance.isRespawning = true;
                Player.instance.playerSpriteRenderer.enabled = false;

                CameraController.instance.isFollowingTarget = false;

                AdManager.instance.PlayRewardedVideoAd();
                
                lives--;
            }
            else {
                // LEGIT GAME OVER
            }

 
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}