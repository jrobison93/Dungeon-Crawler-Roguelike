using System.Collections;

public abstract class StatisticInterface : Subject
{
    protected float totalValue;
    protected float currentValue;

    protected ArrayList observersList;

    public StatisticInterface(float baseValue)
    {
        currentValue = baseValue;
        totalValue = baseValue;
        observersList = new ArrayList();
    }

    // Use this for initialization
    void Start ()
    {
	
	}

    public virtual void ReduceValue(float value)
    {
        currentValue -= value;
        NotifyObservers();
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

    public void AddObserver(Observer observer)
    {
        observersList.Add(observer);
    }
    public void RemoveObserver(Observer observer)
    {
        observersList.Remove(observer);
    }


    public abstract void AddValue(float amount);
    public abstract void IncreaseTotal(float amount);
    public abstract void NotifyObservers();
}
