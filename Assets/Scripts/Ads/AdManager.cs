using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager instance;
    public string appStoreId;
    public string playStoreId;

    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore, isTestAd, isRewardVidPlayed;

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

    public void PlayInterstitialAd() {
        // Mute and pause

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
                break;
            case ShowResult.Skipped:
                // CHECK if rewarded, skipped game over
            case ShowResult.Finished:
                if (placementId == rewardedVideoAd) {
                    isRewardVidPlayed = true;
                }
                if (placementId == interstitialAd) {
                    Debug.Log("Finished interstitial");
                }
                break;
            // DEFAULT GAME OVER
        }
    }    

}
