using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollManager : MonoBehaviour
{
#pragma warning disable IDE1006 // Naming Styles
    private static List<IPurchasable> potentialShopElements => new()
    {
        // pokemons
        new Altaria(),
        new Azumarill(),
        new Azurill(),
        new Breloom(),
        new Bulbasaur(),
        new Chiyu(),
        new Corphish(),
        new Corsola(),
        new Corsola_G(),
        new Cottonee(),
        new Crustle(),
        new Deino(),
        new Diancie(),
        new Dratini(),
        new Dreepy(),
        new Dwebble(),
        new Flygon(),
        new Galvantula(),
        new Glimmet(),
        new Glimmora(),
        new Hatenna(),
        new Hattrem(),
        new Hatterene(),
        new IronBundle(),
        new IronValiant(),
        new Ivysaur(),
        new Jirachi(),
        new Joltik(),
        new Kyogre(),
        new Latias(),
        new Litwick(),
        new Magearna(),
        new Manaphy(),
        new Mantyke(),
        new Mareanie(),
        new Marill(),
        new Mawile(),
        new Porygon(),
        new Porygon2(),
        new PorygonZ(),
        new Regieleki(),
        new Shaymin(),
        new Shroomish(),
        new SlitherWing(),
        new Sneasel(),
        new Starly(),
        new Starmie(),
        new Staryu(),
        new Swablu(),
        new Tinkatink(),
        new Trapinch(),
        new Vaporeon(),
        new Vibrava(),
        new Victini(),
        new Whimsicott(),
        new Wochien(),

        // items
        // new Leftovers()
    };
#pragma warning restore IDE1006 // Naming Styles
    public static List<IPurchasable> PotentialShopElements { get => new(potentialShopElements); }

    public static IPurchasable RollAnItem(int shopTier)
    {
        //int shopTier = math.min((GameManager.turn / 2) + 1, 6);
        List<IPurchasable> availablePool = RollManager.PotentialShopElements.Where(potentialItem => potentialItem.Tier <= shopTier).ToList(); // creates a pool of rollable items
        System.Random random = new(); // makes an rng
        return availablePool[random.Next(0, availablePool.Count)]; // gets a random shopitem
    }

}
