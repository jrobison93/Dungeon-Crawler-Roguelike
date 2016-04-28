using UnityEngine;
using System.Collections;

public class Meteor : Special
{
    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        specialAbilityPath = "Prefabs/Specials/Meteor";
        base.Cast(origin, direction, specialMod);
    }
}
