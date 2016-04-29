using UnityEngine;
using System.Collections;

public class Meteor : Special
{
    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        specialAbilityPath = "Prefabs/Specials/Meteor";
        Special special = InstantiateSpecial(origin, direction);

        special.xDir = (int)direction.x;
        special.yDir = (int)direction.y;
        special.modifier = specialMod;
        special.SetUpSprite();
    }
}
