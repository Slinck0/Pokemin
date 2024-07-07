﻿using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public IPurchasable? shopItem;
    public GameObject shopPanelTileIcon;
    public static bool buying = false;
    public void OnClick()
    {
        if (!buying)
        {
            buying = true;
            Shop.ShopInstance.shopText.SetActive(true); // display shop text
            Shop.ShopInstance.ItemToPurchase = this;
        } else
        {
            buying = false;
            Shop.ShopInstance.shopText.SetActive(false); // turn off shop text
            Shop.ShopInstance.ItemToPurchase = null;

        }
    }

    public void AfterPurchase()
    {
        if (GameManager.whosTurn.Energy >= 2) // if the purchase went through
        {
            GameManager.whosTurn.Energy -= 2;
            shopItem = null;
            GetComponent<Image>().enabled = false;
            shopPanelTileIcon.GetComponent<Image>().enabled = false;
        }

        buying = false;
        Shop.ShopInstance.shopText.SetActive(false); // turn off shop text

    }
}


