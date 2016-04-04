using System;

public class PlayerMana : StatisticInterface
{
    public PlayerMana(float baseMana) : base(baseMana) { }

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
    }

    public override void IncreaseTotal(float manaMod)
    {
        float manaIncrease = (float)Math.Log(manaMod, 2);
        totalValue += manaIncrease;
        currentValue += manaIncrease;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
