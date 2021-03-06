﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Vector2 startPosition;
    private int lives = 1;


    public int distanceTraveled;

    public static LevelManager instance;

    public GameObject player;
    public TextMeshProUGUI odometerDisplay, centrePrompt, doubleJumpText, currentScore, hiScoreText, adHiScoreText, adCurrentScore;

    public int doubleJumpsAvailable;

    public RawImage gameOverPrompt, watchAdPrompt;

    public Transform gameOverPromptTarget;

    private void Awake() {
        instance = this;
    }
    
    void Start()
    {
        AudioManager.instance.PlayBGM();

        startPosition = player.transform.position;
        StartCoroutine(PreStartCountdown()); 
    }

    void Update()
    {
        if (!Player.instance.isFrozen) {
            distanceTraveled = (int)Vector2.Distance(startPosition, player.transform.position);
    
            UpdateOdometer();

            if (distanceTraveled % 300 == 0 && distanceTraveled > 0 && Player.instance.maxSpeed > Player.instance.initialSpeed) {
                Player.instance.initialSpeed += 0.1f;
            }
        }

        if (Player.instance.isRespawning) {
            centrePrompt.gameObject.SetActive(true);
            centrePrompt.text = "TAP A SPOT TO RESPAWN";
        }
        
    }

    void UpdateOdometer() {
        odometerDisplay.text = distanceTraveled.ToString();
        currentScore.text = distanceTraveled.ToString();
        adCurrentScore.text = distanceTraveled.ToString();
    }

    public IEnumerator PreStartCountdown() {

        Player.instance.isFrozen = true;

        centrePrompt.gameObject.SetActive(true);
        centrePrompt.text = "3";
        AudioManager.instance.PlaySFX(5);

        yield return new WaitForSeconds(1);
        centrePrompt.text = "2";
        AudioManager.instance.PlaySFX(5);

        yield return new WaitForSeconds(1);
        centrePrompt.text = "1";
        AudioManager.instance.PlaySFX(5);

        yield return new WaitForSeconds(1);
        AudioManager.instance.PlaySFX(5);
        centrePrompt.text = "RUN";
        yield return new WaitForSeconds(1);
        centrePrompt.gameObject.SetActive(false);


        Player.instance.isFrozen = false;

    }

    public IEnumerator HandleGameOver(int waitTime) {

        if (lives > 0) {
            yield return new WaitForSeconds(waitTime);

            FreezePlayer(true);

            PromptAd();


            lives--;
        }
        else {
            FreezePlayer(false);
            PromptGameOver();
        }

 
    }

    public void FreezePlayer(bool isRespawning) {
        Player.instance.playerRigidbody.velocity = new Vector2(0, Player.instance.playerRigidbody.velocity.y);
        Player.instance.isFrozen = true;
        Player.instance.playerSpriteRenderer.enabled = false;

        CameraController.instance.isFollowingTarget = false;
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    public void UpdateDoubleJumpText() {
        doubleJumpText.text = "DOUBLE JUMPS X " + doubleJumpsAvailable.ToString();
    }

    public void PromptGameOver() {
        UpdateHiScore();
        gameOverPrompt.transform.position = Vector3.MoveTowards(transform.position, gameOverPromptTarget.position, 2 * Time.deltaTime);
        Time.timeScale = 0;
    }

    public void PromptAd() {
        UpdateHiScore();
        watchAdPrompt.transform.position = Vector3.MoveTowards(transform.position, gameOverPromptTarget.position, 2 * Time.deltaTime);
    }

    public void UpdateHiScore() {
        int hiScore = PlayerPrefs.GetInt("HiScore", 0);

        if (hiScore < distanceTraveled) {
            hiScore = distanceTraveled;
            
            PlayerPrefs.SetInt("HiScore", hiScore);

        }

        hiScoreText.text = hiScore.ToString();
        adHiScoreText.text = hiScore.ToString();
    }

}