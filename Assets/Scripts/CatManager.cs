using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public AudioSource[] catSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandomCatSound(){
    //     if (PlayerPrefs.HasKey("Sound")) {
    //         if (PlayerPrefs.GetInt("Sound") == 1) {
    //             // choose a random number
    //             int clipToPlay = Random.Range(0, catSound.Length);
    //             // play the clip
    //             catSound[clipToPlay].Play();
    //         }  
    //     } else {
    //         // choose a random number
    //         int clipToPlay = Random.Range(0, catSound.Length);
    //         // play the clip
    //         catSound[clipToPlay].Play();
    //     }
    // }
        // Debug.Log('the cat!!!!!!!!!!!');
        int clipToPlay = Random.Range(0, catSound.Length);
        catSound[clipToPlay].Play();
    }

}
