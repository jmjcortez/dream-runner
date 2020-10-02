using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ExitToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void PrepareRewardedAd() {
        LevelManager.instance.watchAdPrompt.gameObject.SetActive(false);
        // Player.instance.isRespawning = true;
        AdManager.instance.PlayRewardedVideoAd();
    }

}
