using System;
using UnityEngine;

public class PlayerHealth : StatisticInterface
{
    private int numberOfCollectedPotions = 0;
    private int numberOfCollectedDefenseMods = 0;

    public PlayerHealth(float baseHealth) : base(baseHealth)
    {
        GameObject GUI = GameObject.FindGameObjectWithTag("GUI");
        base.AddObserver(GUI.GetComponent<GUIUpdater>());
    }

    public override void AddValue(float value)
    {
        if (currentValue + value < totalValue)
        {
            currentValue += value;
        }
        else
        {
            currentValue = totalValue;
        }

        numberOfCollectedPotions++;
        NotifyObservers();
    }

    public override void IncreaseTotal(float defenseMod)
    {
        float defenseIncrease = (float)Math.Log(defenseMod, 2);
        totalValue += defenseIncrease;
        currentValue += defenseIncrease;

        numberOfCollectedDefenseMods++;

        NotifyObservers();
    }

    public override void NotifyObservers()
    {
        object[] args = new object[] { currentValue, totalValue, numberOfCollectedPotions, numberOfCollectedDefenseMods };
        foreach(Observer observer in observersList)
        {
            observer.UpdateObserver(this, args);
        }
    }
}
