using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class trophyManager : MonoBehaviour
{
    private GameData gameData;
    public GameObject[] tropyhButtons;


    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        trophyUSA();
        trophyChina();
        trophyJapan();
        trophyHalloween();
        trophyX();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void trophyUSA(){
        int flag1=1;
        if(gameData!=null){
            for(int i=0; i<9; i++){
                if(gameData.saveData.isActive[i]==false){
                    flag1=0;
                    break;
                }
            }
            if(flag1==1){
                tropyhButtons[0].SetActive(true);
            }
            else{
                tropyhButtons[0].SetActive(false);
            }
        }
    }

    public void trophyChina(){
        int flag2=1;
        if(gameData!=null){
            for(int i=9; i<18; i++){
                if(gameData.saveData.isActive[i]==false){
                    flag2=0;
                    break;
                }
            }
            if(flag2==1){
                tropyhButtons[1].SetActive(true);
            }
            else{
                tropyhButtons[1].SetActive(false);
            }
        }
    }

    public void trophyJapan(){
        int flag3=1;
        if(gameData!=null){
            for(int i=18; i<27; i++){
                if(gameData.saveData.isActive[i]==false){
                    flag3=0;
                    break;
                }
            }
            if(flag3==1){
                tropyhButtons[2].SetActive(true);
            }
            else{
                tropyhButtons[2].SetActive(false);
            }
        }
    }

    public void trophyHalloween(){
        int flag4=1;
        if(gameData!=null){
            for(int i=27; i<36; i++){
                if(gameData.saveData.isActive[i]==false){
                    flag4=0;
                    break;
                }
            }
            if(flag4==1){
                tropyhButtons[3].SetActive(true);
            }
            else{
                tropyhButtons[3].SetActive(false);
            }
        }
    }

    public void trophyX(){
        int flag5=1;
        if(gameData!=null){
            for(int i=36; i<45; i++){
                if(gameData.saveData.isActive[i]==false){
                    flag5=0;
                    break;
                }
            }
            if(flag5==1){
                tropyhButtons[4].SetActive(true);
            }
            else{
                tropyhButtons[4].SetActive(false);
            }
        }
    }

}
