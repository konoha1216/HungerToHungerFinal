using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType
{
    Moves,
    Time
}

[System.Serializable]
public class EndGameRequirements
{
    public GameType gameType;
    public int counterValue;
}

public class EndGameManager : MonoBehaviour
{

    // [Header ("Scriptable Object Stuff")]
    // public World world;
    // public int level;

    public GameObject movesLabel;
    public GameObject timeLabel;
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public Text counter;
    public EndGameRequirements requirements;
    public int currentCounterValue;
    public static int boostIncreaseCounter = -1;
    private Board board;
    private float timerSeconds;
    // Start is called before the first frame update

    private void SetGameType(){
        if(board.world != null){
            if(board.world.levels[board.level] != null){
                requirements = board.world.levels[board.level].endGameRequirements;
            }
        }
    }

    void Start()
    {
        board = FindObjectOfType<Board>();
        SetGameType();
        SetupGame();
        GameObject.Find("limitsboosterButton").GetComponentInChildren<Text>().text = "Increase\nLeft: " + boostIncreaseCounter.ToString();
    }

    void SetupGame(){
        currentCounterValue = requirements.counterValue;
        if(requirements.gameType == GameType.Moves){
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        }else{
            timerSeconds = 1;
            movesLabel.SetActive(false);
            timeLabel.SetActive(true);
        }

        counter.text = "" + currentCounterValue;
    }

    public void DecreaseCounterValue(){
        if(board.currentState != GameState.pause){
            currentCounterValue--;
            counter.text = ""+currentCounterValue;
            if(currentCounterValue <= 0){
                LoseGame();
            }
        }
    }

    public void BoosterIncreaseCounterValue(){
        if (boostIncreaseCounter == 0) {
            return;
        }
        boostIncreaseCounter--;
        GameObject.Find("limitsboosterButton").GetComponentInChildren<Text>().text = "Increase\nLeft: " + boostIncreaseCounter.ToString();

        if(requirements.gameType == GameType.Time && currentCounterValue > 0){
            currentCounterValue+=10;
            counter.text = ""+currentCounterValue;
        }else{
            currentCounterValue+=3;
            counter.text = ""+currentCounterValue;
        }
    }

    public void WinGame(){
        youWinPanel.SetActive(true);
        board.currentState = GameState.win;
        // currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    public void LoseGame(){
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        Debug.Log("you lose!");
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if(requirements.gameType == GameType.Time && currentCounterValue > 0){
            timerSeconds -= Time.deltaTime;
            if(timerSeconds <= 0){
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
}
