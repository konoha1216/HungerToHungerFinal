using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winingCoin : MonoBehaviour
{
    private EndGameManager endGameManager;
    private ScoreManager scoreManager;
    private EndGameRequirements endGameRequirements;
    public int winCoins;
    // Start is called before the first frame update
    void Start()
    {
        endGameManager=FindObjectOfType<EndGameManager>();
        scoreManager=FindObjectOfType<ScoreManager>();
    }

    void Coinsaward(){
        Debug.Log(endGameRequirements.gameType);
        Debug.Log(endGameManager.currentCounterValue);
        Debug.Log(scoreManager.score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
