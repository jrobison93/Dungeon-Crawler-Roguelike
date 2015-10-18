using UnityEngine;
using System.Collections;
using System;

public class Enemy : MovingObject
{
	
	// Update is called once per frame
	void Update () {

    }

    protected override void OnCantMove<T>(T component)
    {
        throw new NotImplementedException();
    }
}
