using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public EndGameManager endGameManager;
    public ScoreManager scoreManager;
    private EndGameRequirements endGameRequirements;
    public float winCoins;
    public static float money = 0;
    public GameObject[] panels;
    public Text coinAward;
    // Start is called before the first frame update
    void Start()
    {
        // endGameRequirements=FindObjectOfType<EndGameRequirements>();
        endGameManager=FindObjectOfType<EndGameManager>();
        scoreManager=FindObjectOfType<ScoreManager>();
    }
    public void Coinsaward(){
        // Debug.Log(endGameRequirements.gameType);
        Debug.Log("HI");
        if(endGameManager!=null){
            Debug.Log(endGameManager.currentCounterValue);
        }
        if(scoreManager!=null){
            Debug.Log(scoreManager.score);
        }
        if(endGameManager!=null && scoreManager!=null){
            winCoins=Random.Range((endGameManager.currentCounterValue*100+scoreManager.score)/5,(endGameManager.currentCounterValue*100+scoreManager.score)/10);
            money = money + winCoins;
            Debug.Log(winCoins);
        }
        coinAward.text=""+winCoins;
    }
    public void paypalAddMoney() {
        money = money + 200;
    }
    public void checkButton(){
        panels[0].SetActive(false);
    }
}
