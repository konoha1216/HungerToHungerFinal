using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager gameManager;
    [SerializeField]private float money;
    // [SerializeField]private float ItemNumber;
    public Text moneyText;
    // public Text number;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        UpdateUI();
    }
    public void AddMoney(float amount) {
        money += amount;
        UpdateUI();
    }
    public void ReduceMoney(float amount) {
        money -= amount;
        UpdateUI();
    }
    public bool RequestMoney(float amount) {
        if (amount <= money) {
            return true;
        }
        return false;
    }

    // public void SetNumberInfo(float number){
    //     ItemNumber = number;
    // }

    public float GetMoneyInfo(){
        return money;
    }

    public void SetMoneyInfo(float amount) {
        money = amount;
        UpdateUI();
    }
    public void paypalAddMoney() {
        ButtonManager.money = ButtonManager.money + 200;
        money = ButtonManager.money;
        UpdateUI();
    }

    void UpdateUI() {
        moneyText.text = "$" + money.ToString("N2");
        // number.text = "You've got " + ItemNumber;
    }
}
