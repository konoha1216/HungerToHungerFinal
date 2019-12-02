using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class confirmImage : MonoBehaviour
{   
    public Image curPostcard;
    private PostcardsButton postcardsButton;
    private Image toShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update(){
        postcardsButton = FindObjectOfType<PostcardsButton>();
        Debug.Log(postcardsButton);
        curPostcard = GetComponent<Image>();
        if(postcardsButton != null){
            toShow = postcardsButton.postCard;
            curPostcard.sprite = toShow.sprite;
        }
    }
}
