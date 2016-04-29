using UnityEngine;
using System.Collections;

public class SuperSpecial : Special
{
    Special baseSpecial;
    Vector3 up;
    Vector3 down;
    Vector3 left;
    Vector3 right;

    public SuperSpecial(Special baseSpecial)
    {
        this.baseSpecial = baseSpecial;
        up = new Vector3(1, 0, 0);
        down = new Vector3(-1, 0, 0);
        left = new Vector3(0, -1, 0);
        right = new Vector3(0, 1, 0);
    }

    public override void SetBase(Special baseSpecial)
    {
        this.baseSpecial = baseSpecial;
    }

    public override void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        baseSpecial.Cast(origin, up, specialMod);
        baseSpecial.Cast(origin, down, specialMod);
        baseSpecial.Cast(origin, left, specialMod);
        baseSpecial.Cast(origin, right, specialMod);

    }
}
