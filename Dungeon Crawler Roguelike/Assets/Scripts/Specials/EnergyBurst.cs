using UnityEngine;
using System.Collections;

public class EnergyBurst : Special
{
    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        specialAbilityPath = "Prefabs/Specials/EnergyBurst";
        base.Cast(origin, direction, specialMod);
    }

}
