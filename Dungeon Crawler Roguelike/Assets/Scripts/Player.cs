using UnityEngine;
using System.Collections;
using System;

public class Player : MovingObject
{
    private float walkTime = 0.25f;
    private float startTime = Time.time;
	
	// Update is called once per frame
	void Update ()
    {
        if (startTime + walkTime <= Time.time)
        {
            int horizontal = (int)Input.GetAxisRaw("Horizontal");
            int vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                Debug.Log(horizontal + " " + vertical);
                AttemptMove<Enemy>(horizontal, vertical);
            }

            startTime = Time.time;
        }
	
	}

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
