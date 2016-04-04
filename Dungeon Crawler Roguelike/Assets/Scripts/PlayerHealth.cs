using System;

public class PlayerHealth : StatisticInterface
{

    public PlayerHealth(float baseHealth) : base(baseHealth) { }

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
    }

    public override void IncreaseTotal(float defenseMod)
    {
        float defenseIncrease = (float)Math.Log(defenseMod, 2);
        totalValue += defenseIncrease;
        currentValue += defenseIncrease;
    }

    // Use this for initialization
    void Start () {
	
	}
}
