using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public int level;
    
    public void PlayGame () {
        PlayerPrefs.SetInt("Current Level", level - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NextThreePage () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void BackThreePage () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    public void QuitGame () {
        Application.Quit();
    }
    public void Back () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
