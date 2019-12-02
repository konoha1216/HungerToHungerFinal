using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState{
    wait,
    move,
    win,
    lose,
    pause
}

public enum TileKind{
    Breakable,
    Blank,
    Normal
}

[System.Serializable]
public class TileType{
    public int x;
    public int y;
    public TileKind tileKind;
}

public class Board : MonoBehaviour
{
    [Header ("Scriptable Object Stuff")]
    public World world;
    public int level;

    public GameState currentState = GameState.move;
    [Header ("Board Dimensions")]
	public int width;
    public int height;
    public int offSet;

    [Header ("Prefabs")]
    public GameObject tilePrefab;
    public GameObject breakableTilePrefab;
    public GameObject[] dots;
    public GameObject destroyEffect;

    [Header ("Layout")]
    public TileType[] boardLayout;
    private bool[,] blankSpaces;
    private BackgroundTile[,] breakableTiles;
    public GameObject[,] allDots;
    public Dot currentDot;
    private FindMatches findMatches;
    public int basePieceValue = 20;
    private int streakValue = 1;
    private ScoreManager scoreManager;
    private SoundManager soundManager;
    private GoalManager goalManager;
    public float refillDelay = 0.5f;
    public int[] scoreGoals;
    public static int reshuffleIncreaseCounters = -1;


    // [Header("background image")]
    // public BackgroundManager backgroundManager;

    private void Awake(){
        if(PlayerPrefs.HasKey("Current Level")){
            level = PlayerPrefs.GetInt("Current Level");
        }
        if(world != null){
            if(world.levels[level] != null){
                width = world.levels[level].width;
                height = world.levels[level].height;
                dots = world.levels[level].dots;
                scoreGoals = world.levels[level].scoreGoals;
                boardLayout = world.levels[level].boardLayout;
                // backgroundManager = world.levels[level].backgroundManager;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        goalManager = FindObjectOfType<GoalManager>();
        soundManager = FindObjectOfType<SoundManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        breakableTiles = new BackgroundTile[width, height];
        findMatches = FindObjectOfType<FindMatches>();
        blankSpaces = new bool[width, height];
        allDots = new GameObject[width, height];
        SetUp();
        currentState = GameState.pause;
        GameObject.Find("ReshuffleButton").GetComponentInChildren<Text>().text = "Reshuffle\nLeft: " + reshuffleIncreaseCounters.ToString();
    }

    public void GenerateBlankSpaces(){
        for(int i=0; i<boardLayout.Length; i++){
            if(boardLayout[i].tileKind == TileKind.Blank){
                blankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
            }
        }
    }

    public void GenerateBreakableTiles(){
        for(int i=0; i<boardLayout.Length; i++){
            if(boardLayout[i].tileKind == TileKind.Breakable){
                Vector2 tempPosition = new Vector2(boardLayout[i].x, boardLayout[i].y);
                GameObject tile = Instantiate(breakableTilePrefab, tempPosition, Quaternion.identity);
                breakableTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BackgroundTile>();
            }
        }
    }

    private void SetUp(){
        GenerateBlankSpaces();
        GenerateBreakableTiles();
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                if(!blankSpaces[i,j]){
                    Vector2 tempPosition = new Vector2(i,j+offSet);
                    Vector2 tilePosition = new Vector2(i,j);
                    GameObject backgroundTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = "(" + i + ", " + j + ")";
                    int dotToUse = Random.Range(0, dots.Length);

                    int maxIterations = 0;
                    while(MatchesAt(i, j, dots[dotToUse]) && maxIterations<100){
                        dotToUse = Random.Range(0, dots.Length);
                        maxIterations++;
                        // Debug.Log(maxIterations);
                    }
                    maxIterations=0;

                    GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    dot.GetComponent<Dot>().row=j;
                    dot.GetComponent<Dot>().column=i;

                    dot.transform.parent = this.transform;
                    dot.name = "(" + i + ", " + j + ")";

                    allDots[i,j] = dot;
                }
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece){
        if(column>1 && row>1){
            if(allDots[column-1, row] != null && allDots[column-2, row] != null){
                if(allDots[column-1, row].tag == piece.tag && allDots[column-2, row].tag == piece.tag){
                    return true;
                }
            }
            if(allDots[column, row-1] != null && allDots[column, row-2] != null){
                if(allDots[column, row-1].tag == piece.tag && allDots[column, row-2].tag == piece.tag){
                    return true;
                }
            }
        }else if(column<=1 || row<=1){
            if(row>1){
                if(allDots[column, row-1] != null && allDots[column, row-2] != null){
                    if(allDots[column,row-1].tag == piece.tag && allDots[column, row-2].tag==piece.tag){
                        return true;
                    }
                }
            }
            if(column>1){
                if(allDots[column-1, row] != null && allDots[column-2, row] != null){
                    if(allDots[column-1,row].tag == piece.tag && allDots[column-2, row].tag==piece.tag){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private int ColumnOrRow(){
        List<GameObject> matchCopy = findMatches.currentMatches as List<GameObject>;
        for(int i=0; i<matchCopy.Count; i++){
            Dot thisDot = matchCopy[i].GetComponent<Dot>();

            int column = thisDot.column;
            int row = thisDot.row;
            int columnMatch=0;
            int rowMatch=0;
            for(int j=0; j<matchCopy.Count; j++){
                Dot nextDot = matchCopy[j].GetComponent<Dot>();
                if(nextDot==thisDot){
                    continue;
                }
                if(nextDot.column==thisDot.column && nextDot.CompareTag(thisDot.tag)){
                    columnMatch++;
                }
                if(nextDot.row==thisDot.row && nextDot.CompareTag(thisDot.tag)){
                    rowMatch++;
                }
            }

            if(columnMatch==4 || rowMatch==4){
                return 1;
            }
            if(columnMatch==2 && rowMatch==2){ 
                return 2;
            }
            if(columnMatch==3 || rowMatch==3){
                return 3;
            }
        }
        return 0;
    }

    private void CheckToMakeBombs(){
        if(findMatches.currentMatches.Count>3){
            int typeOfMatch = ColumnOrRow();
            if(typeOfMatch==1){
                if(currentDot != null){
                    if(currentDot.isMatched){
                        if(!currentDot.isColorBomb){
                            currentDot.isMatched = false;
                            currentDot.MakeColorBomb();
                        }
                    }else{
                        if(currentDot.otherDot != null){
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if(otherDot.isMatched){
                                if(!otherDot.isColorBomb){
                                    otherDot.isMatched = false;
                                    otherDot.MakeColorBomb();
                                }
                            }
                        }
                    }
                }
            }
            else if(typeOfMatch==2){
                if(currentDot != null){
                    if(currentDot.isMatched){
                        if(!currentDot.isAdjacentBomb){
                            currentDot.isMatched = false;
                            currentDot.MakeAdjacentBomb();
                        }
                    }else{
                        if(currentDot.otherDot != null){
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if(otherDot.isMatched){
                                if(!otherDot.isAdjacentBomb){
                                    otherDot.isMatched = false;
                                    otherDot.MakeAdjacentBomb();
                                }
                            }
                        }
                    }
                }
            }
            else if(typeOfMatch==3){
                findMatches.CheckBombs();
            }
        }
    }

    private void DestroyMatchesAt(int column, int row){
        if(allDots[column, row].GetComponent<Dot>().isMatched){
            if(findMatches.currentMatches.Count >= 4){
                CheckToMakeBombs();
            }
            
            if(breakableTiles[column, row] != null){
                breakableTiles[column, row].TakeDamage(1);
                if(breakableTiles[column, row].hitPoints <= 0){
                    breakableTiles[column, row] = null;
                }
            }

            if(goalManager != null){
                goalManager.ComparaGoal(allDots[column, row].tag.ToString());
                goalManager.UpdateGoals();
            }

            // does the sound manager exist?
            if(soundManager != null){
                soundManager.PlayRandomDestroyNoise();
            }
            GameObject particle = Instantiate(destroyEffect, allDots[column,row].transform.position, Quaternion.identity);
            Destroy(particle, .5f);
            Destroy(allDots[column, row]);
            scoreManager.IncreaseScore(basePieceValue*streakValue);
            allDots[column, row] = null;
        }
    }

    public void DestroyMatches(){
        for(int i=0; i<width; i++){
            for(int j=0; j< height; j++){
                if(allDots[i,j] != null){
                    DestroyMatchesAt(i,j);
                }
            }
        }
        findMatches.currentMatches.Clear();
        StartCoroutine(DecreaseRowCo2());
    }

    private IEnumerator DecreaseRowCo2(){
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                // not blank and is empty
                if(!blankSpaces[i,j] && allDots[i,j] == null){
                    for(int k=j+1; k<height; k++){
                        if(allDots[i,k] != null){
                            allDots[i,k].GetComponent<Dot>().row = j;
                            allDots[i,k] = null;
                            break;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(refillDelay * 0.5f);
        StartCoroutine(FillBoardCo());
    }

    private IEnumerator DecreaseRowCo(){
        int nullCount = 0;
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                if(allDots[i,j] == null){
                    nullCount++;
                }else if(nullCount>0){
                    allDots[i,j].GetComponent<Dot>().row -= nullCount;
                    allDots[i,j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(refillDelay * 0.5f);
        StartCoroutine(FillBoardCo());
        // FillBoardCo();
    }

    private void RefillBoard(){
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                if(allDots[i,j] == null && !blankSpaces[i,j]){
                    Vector2 tempPosition = new Vector2(i,j+offSet);
                    int dotToUse = Random.Range(0, dots.Length);
                    int maxIterations = 0;

                    while(MatchesAt(i,j,dots[dotToUse]) && maxIterations<100){
                        maxIterations++;
                        dotToUse = Random.Range(0, dots.Length);
                    }

                    maxIterations = 0;
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i,j] = piece;
                    piece.GetComponent<Dot>().row=j;
                    piece.GetComponent<Dot>().column=i;
                }
            }
        }
    }

    private bool MatchesOnBoard(){
        for(int i =0; i<width; i++){
            for(int j=0; j<height; j++){
                if(allDots[i,j] != null){
                    if(allDots[i,j].GetComponent<Dot>().isMatched){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo(){
        RefillBoard();
        yield return new WaitForSeconds(refillDelay);
        
        while(MatchesOnBoard()){
            streakValue++;
            DestroyMatches();
            yield return new WaitForSeconds(2*refillDelay);
        }
        findMatches.currentMatches.Clear();
        currentDot = null;
        

        if(IsDeadlocked()){
            StartCoroutine(ShuffleBoard());
            // ShuffleBoard();
            Debug.Log("deadlocked!");
        }
        yield return new WaitForSeconds(refillDelay);
        if (currentState != GameState.pause)
            currentState = GameState.move;
        
        streakValue = 1;
    }

    private void SwitchPieces(int column, int row, Vector2 direction){
        // take the second piece and save it in a holder
        GameObject holder = allDots[column +(int)direction.x, row+(int)direction.y] as GameObject;
        // switch the first dot to be the second position
        allDots[column+(int)direction.x, row+(int)direction.y] = allDots[column, row];
        // set the first dot to be the second dot
        allDots[column, row] = holder;
    }

    private bool CheckForMatches(){
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                if(allDots[i,j] != null){
                    //make sure one and two to the right are in the board
                    if(i<width-2){
                    // check if the dots to the right and two to the right exist
                        if(allDots[i+1, j]!=null && allDots[i+2, j]!=null){
                            if(allDots[i+1, j].tag == allDots[i,j].tag && allDots[i+2,j].tag == allDots[i,j].tag){
                                return true;
                            }
                        }
                    }
                    if(j<height-2){
                        // check if the dots above exist
                        if(allDots[i,j+1] != null && allDots[i,j+2] != null){
                            if(allDots[i, j+1].tag == allDots[i,j].tag && allDots[i,j+2].tag == allDots[i,j].tag){
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool SwitchAndCheck(int column, int row, Vector2 direction){
        SwitchPieces(column, row, direction);
        if(CheckForMatches()){
            SwitchPieces(column, row, direction);
            return true;
        }
        SwitchPieces(column, row, direction);
        return false;
    }

    private bool IsDeadlocked(){
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                if(allDots[i,j]!=null){
                    if(i<width-1){
                        if(SwitchAndCheck(i,j, Vector2.right)){
                            return false;
                        }
                    }
                    if(j<height-1){
                        if(SwitchAndCheck(i,j, Vector2.up)){
                            return false;
                        }
                    }
                } 
            }
        }
        return true;
    }

    private IEnumerator ShuffleBoard(){
        yield return new WaitForSeconds(0.5f);
        // create a list of game objects
        List<GameObject> newBoard = new List<GameObject>();
        // add every piece to this list
        for(int i=0; i<width; i++){
            for(int j=0; j<width; j++){
                if(allDots[i,j] != null){
                    newBoard.Add(allDots[i,j]);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        // for every spot on the board
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                // this tile is not a blank one
                if(!blankSpaces[i,j]){
                    // pick a random number
                    int pieceToUse = Random.Range(0,newBoard.Count);
                    
                    // assign the column and row to the piece

                    int maxIterations = 0;
                    while(MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations<100){
                        pieceToUse = Random.Range(0, newBoard.Count);
                        maxIterations++;
                        // Debug.Log(maxIterations);
                    }
                    // make a container for the piece
                    Dot piece=newBoard[pieceToUse].GetComponent<Dot>();
                    maxIterations=0;

                    piece.column = i;
                    piece.row = j;
                    // fill in the dots array with the new piece, then remove it from the list
                    allDots[i,j] = newBoard[pieceToUse];
                    newBoard.Remove(newBoard[pieceToUse]);
                }
            }
        }
        // check if still deadlocked
        if(IsDeadlocked()){
            StartCoroutine(ShuffleBoard());
        }
    }

    public void makeReshuffle(){
        if (reshuffleIncreaseCounters == 0) {
            return;
        }
        reshuffleIncreaseCounters --;
        GameObject.Find("ReshuffleButton").GetComponentInChildren<Text>().text = "Reshuffle\nLeft: " + reshuffleIncreaseCounters.ToString();

        // create a list of game objects
        List<GameObject> newBoard = new List<GameObject>();
        // add every piece to this list
        for(int i=0; i<width; i++){
            for(int j=0; j<width; j++){
                if(allDots[i,j] != null){
                    newBoard.Add(allDots[i,j]);
                }
            }
        }
        // for every spot on the board
        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                // this tile is not a blank one
                if(!blankSpaces[i,j]){
                    // pick a random number
                    int pieceToUse = Random.Range(0,newBoard.Count);
                    
                    // assign the column and row to the piece

                    int maxIterations = 0;
                    while(MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations<100){
                        pieceToUse = Random.Range(0, newBoard.Count);
                        maxIterations++;
                        // Debug.Log(maxIterations);
                    }
                    // make a container for the piece
                    Dot piece=newBoard[pieceToUse].GetComponent<Dot>();
                    maxIterations=0;

                    piece.column = i;
                    piece.row = j;
                    // fill in the dots array with the new piece, then remove it from the list
                    allDots[i,j] = newBoard[pieceToUse];
                    newBoard.Remove(newBoard[pieceToUse]);
                }
            }
        }
    }

}
