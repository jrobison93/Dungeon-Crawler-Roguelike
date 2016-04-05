using UnityEngine;

public class Potion : MonoBehaviour
{
    protected float value = 10.0f;

    public virtual float GetValue()
    {
        return value;
    }
}
