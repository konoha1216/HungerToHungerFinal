using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftShop : MonoBehaviour
{
    public static GiftShop giftShop;
    public List<Gift> giftList = new List<Gift>();
    public GameObject itemHolderPrefab;
    public Transform grid;

    // Start is called before the first frame update
    void Start()
    {
        giftShop = this;
        FillList();
    }
    void FillList() {
        for (int i = 0; i < giftList.Count; i++) {
            GameObject holder = Instantiate(itemHolderPrefab,grid,false);
            ItemHolder holderScript = holder.GetComponent<ItemHolder>();
            holderScript.itemName.text = giftList[i].giftName;
            holderScript.itemPrice.text = "$ " + giftList[i].giftPrice.ToString("N2");
            holderScript.itemID = giftList[i].giftID;
            holderScript.number = giftList[i].number;

            // buy button
            holderScript.buyButton.GetComponent<BuyButton>().giftID = giftList[i].giftID;

            // if (giftList[i].bought) {
            //     holderScript.itemImage.sprite = giftList[i].boughtSprite;
            // } else {
            //     holderScript.itemImage.sprite = giftList[i].unboughtSprite;
            // }
        }
    }
}
