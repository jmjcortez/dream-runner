using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager instance;
    public string appStoreId;
    public string playStoreId;

    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore, isTestAd, isRewardVidPlayed, isExitingToMainMenu = false;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Advertisement.AddListener(this);
        InitializeAdvertisement();
    }

    private void InitializeAdvertisement() {
        if (isTargetPlayStore) {
            Advertisement.Initialize(playStoreId, isTestAd);
            return;
        }

        Advertisement.Initialize(appStoreId, isTestAd);
    }

    public void PlayInterstitialAd(bool willExit=false) {
        // Mute and pause

        isExitingToMainMenu = willExit;

        if (Advertisement.IsReady(interstitialAd)) {
            Advertisement.Show(interstitialAd);
        }
    }

    public void PlayRewardedVideoAd() {
        // Mute and pause

        if (Advertisement.IsReady(rewardedVideoAd)) {
            Advertisement.Show(rewardedVideoAd);
        }
    }

    public void OnUnityAdsReady(string placementId) {

    }

    public void OnUnityAdsDidError(string message) {

    }

    public void OnUnityAdsDidStart(string placementId) {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        switch(showResult) {
            case ShowResult.Failed:
                ReloadScene();
                break;
            case ShowResult.Skipped:
                WatchedInterstitialAdLogic();
                break;
            case ShowResult.Finished:
                if (placementId == rewardedVideoAd) {
                    isRewardVidPlayed = true;
                    Player.instance.isRespawning = true;
                }
                if (placementId == interstitialAd) {
                    WatchedInterstitialAdLogic();
                }
                break;
        }
    }

    void WatchedInterstitialAdLogic() {
        if (isExitingToMainMenu) {
            SceneManager.LoadScene("MainMenu");
            isExitingToMainMenu = false;
        }
        else {
            ReloadScene();
        }
    }
    public void ReloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

}
