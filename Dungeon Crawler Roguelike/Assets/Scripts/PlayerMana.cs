using System;
using UnityEngine;

public class PlayerMana : StatisticInterface
{
    private int numberOfCollectedPotions = 0;
    private int numberOfCollectedManaMods = 0;

    public PlayerMana(float baseMana) : base(baseMana)
    {
        GameObject GUI = GameObject.FindGameObjectWithTag("GUI");
        AddObserver(GUI.GetComponent<GUIUpdater>());
    }

    public override void AddValue(float amount)
    {
        if (currentValue + amount < totalValue)
        {
            currentValue += amount;
        }
        else
        {
            currentValue = totalValue;
        }

        numberOfCollectedPotions++;

        NotifyObservers();
    }

    public override void IncreaseTotal(float manaMod)
    {
        float manaIncrease = (float)Math.Log(manaMod, 2);
        totalValue += manaIncrease;
        currentValue += manaIncrease;
        numberOfCollectedManaMods++;

        NotifyObservers();
    }

    public override void NotifyObservers()
    {
        object[] args = new object[] { currentValue, totalValue, numberOfCollectedPotions, numberOfCollectedManaMods };
        foreach (Observer observer in observersList)
        {
            observer.UpdateObserver(this, args);
        }
    }
}
