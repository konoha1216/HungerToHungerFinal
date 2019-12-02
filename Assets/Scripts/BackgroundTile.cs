using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{

    public int hitPoints;
    private SpriteRenderer sprite;
    private GoalManager goalManager;

    void Start(){
        goalManager = FindObjectOfType<GoalManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if(hitPoints <= 0){
            if(goalManager != null){
                goalManager.ComparaGoal(this.gameObject.tag);
                goalManager.UpdateGoals();
            }
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage){
        hitPoints -= damage;
        MakeLighter();
    }

    void MakeLighter(){
        Color color = sprite.color;
        // make the color lighter;
        float newAlpha = color.a * .5f;
        sprite.color = new Color(color.r, color.g, color.b, newAlpha);
    }
    /* 
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame

    void Initialize(){
        
    }
    */
}
