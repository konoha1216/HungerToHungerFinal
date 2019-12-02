using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostcardsButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;
    // public Text descriptText;

    [Header ("Postcards UI")]
    public int PostcardIndex;
    public Image postCard;
    public Text title;
    public Text description;
    public GameObject confirmPanel;

    private GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        // ActivateImages();
        LoadData();
        DecideSprite();
    }

    void LoadData(){
        if(gameData != null){
            // if the postcard is collected
            if(gameData.saveData.isActive[PostcardIndex-1]){
                isActive = true;
            }else{
                isActive = false;
            }
        }
    }
/*
    void ActivateImages(){
        // come back when the binary is done
        postCard.enabled = false;
    }
*/

    void DecideSprite(){
        if(isActive){
            buttonImage.sprite = activeSprite;
            myButton.enabled = true;
            title.enabled = true;
            postCard.enabled = true;
        }else{
            buttonImage.sprite = lockedSprite;
            myButton.enabled = false;
            title.enabled = false;
            postCard.enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmPanel(){
        confirmPanel.SetActive(true);
    }
}
