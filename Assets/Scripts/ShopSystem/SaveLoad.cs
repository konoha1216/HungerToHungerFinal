using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class SaveData {
        public List<Gift> shopList = new List<Gift>();
        public float money;
    }
    void Start() {
        StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(0);
        Loading();
        print(Time.time);
    }
    // public List<Gift> shopList = new List<Gift>();

    public void Saving() {
        SaveData data = new SaveData();
        for (int i = 0; i < GiftShop.giftShop.giftList.Count; i++) {
            print(GiftShop.giftShop.giftList[i].number);
            if (i == 0) {
                EndGameManager.boostIncreaseCounter = GiftShop.giftShop.giftList[i].number;
            }
            if (i == 1) {
                Board.reshuffleIncreaseCounters = GiftShop.giftShop.giftList[i].number;
            }
            data.shopList.Add(GiftShop.giftShop.giftList[i]);
        }
        data.money = Manager.gameManager.GetMoneyInfo();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/shop3.dat", FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();
        print("Saved!");
    }

    public void Loading() {
        if (File.Exists(Application.persistentDataPath + "/shop3.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/shop3.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(stream);
            for (int i = 0; i < data.shopList.Count; i++) {
                GiftShop.giftShop.giftList[i] = data.shopList[i];
                if (i == 0 && EndGameManager.boostIncreaseCounter != -1) {
                    GiftShop.giftShop.giftList[i].number = EndGameManager.boostIncreaseCounter;
                }
                if (i == 1 && Board.reshuffleIncreaseCounters != -1) {
                    GiftShop.giftShop.giftList[i].number = Board.reshuffleIncreaseCounters;
                }

                // Manager.gameManager.SetNumberInfo(GiftShop.giftShop.giftList[i].number);
            }
            Manager.gameManager.SetMoneyInfo(data.money);
            if (ButtonManager.money == 0) {
                ButtonManager.money = data.money;
            } else {
                Manager.gameManager.SetMoneyInfo(ButtonManager.money);
            }
            
            stream.Close();
            print("loaded!");
        } else {
            print("No File Found");
        }
    }
    public void delete(){
        if (File.Exists(Application.persistentDataPath + "/shop3.dat")) {
            File.Delete(Application.persistentDataPath + "/shop3.dat");
        }
    }
}
