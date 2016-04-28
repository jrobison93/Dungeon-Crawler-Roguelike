using UnityEngine;
using System.Collections;

public class PoisonThing : Special
{
    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        specialAbilityPath = "Prefabs/Specials/PoisonThing";
        base.Cast(origin, direction, specialMod);
    }
}
