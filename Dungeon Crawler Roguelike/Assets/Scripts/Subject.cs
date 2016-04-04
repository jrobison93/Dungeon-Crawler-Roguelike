using UnityEngine;
using System.Collections;

public interface Subject
{
    void AddObserver(Observer observer);
    void RemoveObserver(Observer observer);
    void NotifyObservers();
}
