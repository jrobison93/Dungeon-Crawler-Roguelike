using System;
using UnityEngine;

public class EnemyHealth : StatisticInterface
{
    public EnemyHealth(float baseHealth) : base(baseHealth)
    {
        GameObject GUI = GameObject.FindGameObjectWithTag("GUI");
        AddObserver(GUI.GetComponent<GUIUpdater>());
    }

    public override void AddValue(float amount)
    {
        return;
    }

    public override void IncreaseTotal(float amount)
    {
        return;
    }

    public override void NotifyObservers()
    {
        foreach (Observer observer in observersList)
        {
            observer.UpdateObserver(this, null);
        }
    }
}
