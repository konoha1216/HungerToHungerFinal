using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public int giftID;

    public void BuyGift() {
        if (giftID == 0) {
            return;
        }
        for (int i = 0; i < GiftShop.giftShop.giftList.Count; i++) {
            if (GiftShop.giftShop.giftList[i].giftID == giftID && Manager.gameManager.RequestMoney(GiftShop.giftShop.giftList[i].giftPrice)) {
                // GiftShop.giftShop.giftList[i].bought = true;
                GiftShop.giftShop.giftList[i].number++;
                Manager.gameManager.ReduceMoney(GiftShop.giftShop.giftList[i].giftPrice);
            }
        }
    }
}
