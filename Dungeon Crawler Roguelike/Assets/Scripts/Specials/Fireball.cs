using UnityEngine;
using System.Collections;

public class Fireball : Special
{
    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        specialAbilityPath = "Prefabs/Specials/FireBall";
        base.Cast(origin, direction, specialMod);
    }
}
