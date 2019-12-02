using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData{
    public bool[] isActive;
}

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;

    // Start is called before the first frame update
    void Awake()
    {
        if(gameData == null){
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
        }else{
            Destroy(this.gameObject);
        }
        Load();
    }

    private void Start(){
        // Save();

    }

    public void Save(){
        // create a binary formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();
        
        // create a route from the program to the file
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
        Debug.Log(Application.persistentDataPath);
        // create a copy of save data
        SaveData data = new SaveData();
        data = saveData;
        
        // actually save the data to the file
        formatter.Serialize(file, data);
        
        // close the data stream
        file.Close();
        Debug.Log("saved");
    }

    public void Load(){
        // check if the save game file exists
        if(File.Exists(Application.persistentDataPath + "/player.dat")){
            // create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Loaded");
        }
    }

    private void OnDisable(){
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
