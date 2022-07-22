using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// This class contains all variables and methods specific to one currency.
public class Currency
{
    // displayName is what is being shown to the player,
    public string displayName;
    // and currencyName is how you call it in your code.
    public string currencyName;
    // value is the amount you currenly own, this can be changed to other data types if decimals or bigger numbers are needed.
    public int value;

    // Subtracts the amount from value, should be preceded by a CanBuy() check to not go negative.
    public void Buy(int amount)
    {
        value -= amount;
    }
    // Checks if you have enough of that currency to buy something.
    public bool CanBuy (int amount)
    {
        return value - amount >= 0;
    }
    // Increments (This is why it's called an incremntal) the value by amount.
    public void Gain(int amount)
    {
        value += amount;
    }

}

public class CurrencyHandeler : MonoBehaviour
{
    public List<Currency> currencies;
    public Dictionary<string, Currency> currencyMap;

    // Maps al the Currency classes to their name. 
    private void Start()
    {
        foreach (Currency currency in currencies)
        {
            currencyMap.Add(currency.currencyName, currency);
        }
    }
}
