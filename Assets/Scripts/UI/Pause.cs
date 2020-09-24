using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public RawImage pauseMenu;

    public GameObject menus, quitConfirmation, settingsMenu;

    public void PauseGame() {
        Time.timeScale = 0;
        ActivatePrompt();
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        DeactivatePrompt();
    }

    public void ConfirmQuit() {
        menus.SetActive(false);
        quitConfirmation.SetActive(true);
    }

    public void CancelQuit() {
        menus.SetActive(true);
        quitConfirmation.SetActive(false);
        settingsMenu.SetActive(false);
    }
    
    public void ActivatePrompt() {
        pauseMenu.gameObject.SetActive(true);
    }

    public void DeactivatePrompt() {
        pauseMenu.gameObject.SetActive(false);
    }

    public void ShowSettings() {
        menus.SetActive(false);
        settingsMenu.SetActive(true);        
    }
}
