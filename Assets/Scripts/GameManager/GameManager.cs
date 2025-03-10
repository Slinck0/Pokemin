using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public Shop shop;
    public Board board;
    public GameObject die;
    public GameObject energyDisplayText;
    public GameObject numberOfPokemonDisplayText;
    public static (Team, Team) teams = (new Team("Red"), new Team("Blue"));
    public static Team whosTurn = teams.Item1;
    public static int turn = 1;
    public readonly static int MAX_POKEMON = 6;

    private void Start()
    {
        die = GameObject.Find("Reroll");
        whosTurn.Energy = whosTurn.MaxEnergy;
        StartShop();

        Team.EnergyUpdated += (Sender, Energy) => { energyDisplayText.GetComponent<TextMeshProUGUI>().text = Energy.ToString(); }; // fixed it raaaaah
        Team.PokemonCountUpdated += (Sender, PokemonCount) => { numberOfPokemonDisplayText.GetComponent<TextMeshProUGUI>().text = PokemonCount.ToString(); };

        Instance = this;
    }
    public void StartShop()
    {
        try
        {
            Shop.ShopInstance.Reroll();
        }
        catch (NullReferenceException)
        {
            Debug.Log("e");
            
        }
    }

    public void EndTurn()
    {
        // at the end of the turn, each pokemon can start moving
        foreach (Tile tile in board.tiles)
        {
            if (tile.pieceOnTile is not null)
            {
                tile.pieceOnTile.Steps = tile.pieceOnTile.Speed;
                tile.pieceOnTile.moved = false;
            }
        }
        // switching whos turn it is
        if (whosTurn.Name.Equals(teams.Item1.Name))
        {
            whosTurn = teams.Item2;
        }
        else
        {
            whosTurn = teams.Item1;
            turn++;
        }

        whosTurn.Energy = whosTurn.MaxEnergy;
        Team.PokemonCountUpdated.Invoke(this, whosTurn.NumPokemon);

        // handling shopTier upgrades

        if (Shop.shopTier < (int)Math.Min(((double)turn / 2) + .5, 6))
        {
            Shop.shopTier = (int)Math.Min(((double)turn / 2) + .5, 6);
            die.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Dice_Number_" + Shop.shopTier);
        }

        ShopPanel.buying = false;
        Shop.ShopInstance.ItemToPurchase = null;
        Shop.ShopInstance.shopText.SetActive(false);
        InfoUI.Instance.CloseUI();

        Instance.board.ClearHighlightsAndTargets();

    }
}
