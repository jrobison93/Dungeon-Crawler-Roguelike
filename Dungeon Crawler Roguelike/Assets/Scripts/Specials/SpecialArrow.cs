using UnityEngine;
using System.Collections;

public class SpecialArrow : Special
{ 
    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        specialAbilityPath = "Prefabs/Specials/SpecialArrow";
        base.Cast(origin, direction, specialMod);
    }
}
