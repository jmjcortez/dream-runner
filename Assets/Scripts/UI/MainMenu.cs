using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menus, statsMenu, settingsPanel;
    public void ShowStats() {
        menus.SetActive(false);
        statsMenu.SetActive(true);
    }

    public void ReturnToMainMenu() {
        menus.SetActive(true);
        statsMenu.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowSettings() {
        menus.SetActive(false);
        settingsPanel.SetActive(true);        
    }

    public void QuitGame() {
        Application.Quit();
    }
}
