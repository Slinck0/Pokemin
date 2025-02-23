﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public IPurchasable? shopItem; // the item on display
    public GameObject shopPanelTileIcon; // the die
    public static bool buying = false; // whether or not something is in the process of being bought

    public void OnClick()
    {
        GameManager.Instance.board.ClearHighlightsAndTargets(); // technically not necessary but it makes more sense this way
        if (!buying && shopItem != null) // if a shopitem hasnt been chosen yet (and a shopitem is clicked)
        {
            if (GameManager.whosTurn.Energy >= 2 && GameManager.whosTurn.NumPokemon < GameManager.MAX_POKEMON) // if theres enough a energy and less than six pokemon on the team
            {
                buying = true; // now buying!
                if (!GameManager.whosTurn.bought1Item)
                {
                    Shop.ShopInstance.shopText.SetActive(true); // display shop text
                    if (GameManager.whosTurn == GameManager.teams.Item1) // displays different text depending on whos turn it is
                    {
                        Shop.ShopInstance.shopText.GetComponent<TextMeshProUGUI>().text = "Purchase a Pokemon by placing it in a space in one of the bottom two rows!";
                    }
                    else
                    {
                        Shop.ShopInstance.shopText.GetComponent<TextMeshProUGUI>().text = "Purchase a Pokemon by placing it in a space in one of the top two rows!";
                    }
                }
                
                Shop.ShopInstance.ItemToPurchase = this; // this shopitem is now being purchased
            }
            InfoUI.Instance.OpenUI(shopItem);
        }
        else
        {
            buying = false; // no longer buying
            Shop.ShopInstance.shopText.SetActive(false); // turn off shop text
            Shop.ShopInstance.ItemToPurchase = null; // stops trying to buy a shopitem
            InfoUI.Instance.CloseUI();

        }
    }

    public void AfterPurchase() // some cleanup
    {
        if (!GameManager.whosTurn.bought1Item)
        {
            GameManager.whosTurn.bought1Item = true;
            Shop.ShopInstance.shopText.SetActive(false); // turn off shop text
        }
        if (GameManager.whosTurn.Energy >= 2 && GameManager.whosTurn.NumPokemon < GameManager.MAX_POKEMON) // if the purchase went through
        {
            GameManager.whosTurn.Energy -= 2; // decrements energy by 2
            GameManager.whosTurn.NumPokemon++; // increments energy
            shopItem = null; // item is not in the shop anymore
            GetComponent<Image>().enabled = false; // turn off the image
            shopPanelTileIcon.GetComponent<Image>().enabled = false; // turns off the die
        }
        InfoUI.Instance.CloseUI();
        buying = false; // no longer buying anything
        
    }

    public void AfterEvolvingPurchase() // cleanup for evolution specific purchases
    {
        if (GameManager.whosTurn.Energy >= 2) // if the purchase went through
        {
            GameManager.whosTurn.Energy -= 2; // decrements energy by 2
            shopItem = null; // item is not in the shop anymore
            GetComponent<Image>().enabled = false; // turn off the image
            shopPanelTileIcon.GetComponent<Image>().enabled = false; // turns off the die
        }

        buying = false; // no longer buying anything
        Shop.ShopInstance.shopText.SetActive(false); // turn off shop text
    }
}


