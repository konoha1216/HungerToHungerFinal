using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] panels;
    // public GameObject currentPanel;
    // public int page=0;
    // private GameData gameData;
    private Board board;
    // public Image bgimg;

    void Start()
    {
        board = FindObjectOfType<Board>();
        for(int i=0; i<panels.Length; i++){
            panels[i].SetActive(false);
        }

        if(board.world.levels[board.level] != null){
            panels[board.level].SetActive(true);
        }
    }
}
