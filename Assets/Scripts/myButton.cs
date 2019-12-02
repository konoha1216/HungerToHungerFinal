using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class myButton : MonoBehaviour
{
    public int level;
    private GameData gameData;
    private Board board;
    public GameObject[] myText;
    // private float PostcardDelay=3;
    // private int PostcardToUnlock;
    
    public void PlayGame () {
        PlayerPrefs.SetInt("Current Level", level - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // change scenes between postcards scene and menu scene
    public void NextFourPage(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void BackFourPage(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
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
    public void LoseBack(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Back(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);   
    }

    public void check () {
        Debug.Log("hahaha");
        if(gameData != null){
            Debug.Log(board.level);
            int PostcardToUnlock = Random.Range(9*board.level, 9*(board.level+1));
            myText[PostcardToUnlock].SetActive(true);
            gameData.saveData.isActive[PostcardToUnlock] = true;
            Debug.Log(PostcardToUnlock);
            // for(int i=0; i<myText.Length; i++){
            //     myText[i].SetActive(false);
            // }
            // while(PostcardDelay>0){
            //     PostcardDelay -= Time.deltaTime;
            //     Debug.Log(PostcardDelay);
            // }
            gameData.Save();
            
        }
    }

    void Start(){
        gameData = FindObjectOfType<GameData>();
        board = FindObjectOfType<Board>();

        // if(board != null){
        //     PostcardToUnlock = Random.Range(3*board.level, 3*(board.level+1));
        //     Debug.Log(PostcardToUnlock);
        // }
    }
}
