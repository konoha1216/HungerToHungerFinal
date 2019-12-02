using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour
{
/*
    public Image curPostcard;
    public Text curTitle;
    public Text curDescription;
    private PostcardsButton postcardsButton;
    // Start is called before the first frame update
    void Start()
    {
        curPostcard = GetComponent<Image>();
        curTitle = GetComponent<Text>()[0];
        curDescription = GetComponent<Text>[1];
        curPostcard.sprite = postcardsButton.postCard;
        curTitle = postcardsButton.title;
        curDescription = postcardsButton.description;
    }
*/
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel(){
        this.gameObject.SetActive(false);
    }


}
