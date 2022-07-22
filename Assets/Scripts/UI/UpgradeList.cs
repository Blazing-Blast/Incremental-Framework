using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
// How much of which currency the upgrade costs.
public class Cost
{
    public int amount;
    public string name;
    public Currency currency;
}
[System.Serializable]
public class Upgrade
{
    // All the costs of all the currencies combined.
    public List<Cost> combinedCost;
    // A reffernce to another script/GameObject.
    public UpgradeEffects effects;
    // What happens when you try to buy it.
    public void OnBuy()
    {
        // Checks if you have enough of every currrency.
        foreach (Cost cost in combinedCost)
        {
            if (!cost.currency.CanBuy(cost.amount)) return;
            cost.currency.Buy(cost.amount);
        }
        if (!isBuyable.isActive) return;
        /* Here is the place to add your effects like:
        effects.Upgrade1();
        */

        isBought = true;
    }
    // ID of the GameEvent that unlocks this upgrade.
    public int ID;
    public GameEvent isBuyable;

    // This can be replaced by an int (that is --'d in OnBuy()) or you can straigt up remove 'isBought = true;' to be able to purche it multiple times.
    public bool isBought;
    // Unused for now, but may prove usefull when making your own game.
    public string name;
    public string description;
}

public class UpgradeList : MonoBehaviour
{
    // All upgrades that exist in this instance of the script (you could attach it to multiple GameObjects to split the upgradepool up)
    public List<Upgrade> upgradeList;
    // A reffernce to the GameEvent tracker.
    public EventTracker tracker;
    // An array of all the upgrades visable right now.
    public Upgrade[] activeUpgrades;
    // The maximum amount of upgrades in activeUpgrades. 
    public int maxUpgrades;
    // A reffernce to the currency thingy.
    public CurrencyHandeler CurrencyHandeler;
    public void Start()
    {
        // Populates activeUpgrades with all buyable upgrades and fills it with others if there are not enough buyagble ones.
        // Currently only in start() as I didn't have time to properly implement this
        activeUpgrades = new Upgrade[maxUpgrades];
        foreach (Upgrade upgrade in upgradeList)
        {
            upgrade.ID = upgradeList.FindIndex(x => x == upgrade);
            upgrade.isBuyable = tracker.events.Find(x => x.id == upgrade.ID);
            foreach (Cost cost in upgrade.combinedCost)
            {
                cost.currency = CurrencyHandeler.currencyMap[cost.name];
            }
        }
        int UpgradeNumber = 0;
        foreach (Upgrade availableUpgrade in upgradeList.FindAll(x => x.isBuyable.isActive == true && x.isBought == false))
        {
            if (availableUpgrade != null)
            {
                activeUpgrades[UpgradeNumber] = availableUpgrade;
            }
            else if (upgradeList.Find(x => x.isBought == false && !activeUpgrades.Contains(availableUpgrade)) != null) 
            {
                activeUpgrades[UpgradeNumber] = upgradeList.Find(x => x.isBought == false && !activeUpgrades.Contains(availableUpgrade));
            }
            else break;

            UpgradeNumber++;
            if (UpgradeNumber >= maxUpgrades) break;
        }
    }
    public void Update()
    {
        // I should make something similar to what I have in Start() in here.
    }
}
