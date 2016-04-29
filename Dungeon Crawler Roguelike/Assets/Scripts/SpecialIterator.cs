using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialIterator : GameIterator
{
    GameObject[] specials;
    int index = 0;
    int max;
    Image activeSpecial;

    public SpecialIterator(GameObject[] specials)
    {
        this.specials = specials;
        max = this.specials.Length;
        activeSpecial = GameObject.Find("ActiveSpecial").GetComponent<Image>();
    }
    
    public Special Next()
    {
        Special retVal = specials[index].GetComponent<Special>();
        activeSpecial.sprite = retVal.sprites[0];
        Debug.Log("Next");

        index++;
        if(index== max)
        {
            index = 0;
        }

        return retVal;
    }

    public Special Previous()
    {
        index--;
        if (index < 0)
        {
            index = max - 1;
        }
        Debug.Log("Previous");

        Special retVal = specials[index].GetComponent<Special>();
        activeSpecial.sprite = retVal.sprites[0];

        return retVal;
    }
}
