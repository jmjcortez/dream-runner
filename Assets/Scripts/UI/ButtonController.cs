using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{
    public void LoadScene(string loadToString) {
        Time.timeScale = 1;
        SceneManager.LoadScene(loadToString);
    }
}
