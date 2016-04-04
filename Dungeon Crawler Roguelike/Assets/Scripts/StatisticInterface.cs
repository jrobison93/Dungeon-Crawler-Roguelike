public abstract class StatisticInterface
{
    protected float totalValue;
    protected float currentValue;

    public StatisticInterface(float baseValue)
    {
        currentValue = baseValue;
        totalValue = baseValue;
    }

    // Use this for initialization
    void Start () {
	
	}

    public virtual void ReduceValue(float value)
    {
        currentValue -= value;
    }

    public float Percentage()
    {
        return currentValue / totalValue;
    }

    public bool IsDepleted()
    {
        return currentValue <= 0;
    }

    public float CurrentValue()
    {
        return currentValue;
    }

    public abstract void AddValue(float amount);
    public abstract void IncreaseTotal(float amount);


}
