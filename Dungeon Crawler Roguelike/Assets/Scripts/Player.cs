using UnityEngine;
using System.Collections;
using System;

public class Player : MovingObject
{
    public int baseMana;

    private float walkTime = 0.25f;
    private float startTime = Time.time;
    private int totalMana;
    private int currentMana;
	
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
                AttemptMove<Enemy>(horizontal, vertical);
            }

            startTime = Time.time;
        }
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            //Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "HealthSmall")
        {
            AddHealth(10);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "HealthMedium")
        {
            AddHealth(25);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "HealthLarge")
        {
            AddHealth(50);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ManaSmall")
        {
            AddMana(10);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ManaMedium")
        {
            AddMana(25);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ManaLarge")
        {
            AddMana(50);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "AttackUp")
        {
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ManaUp")
        {
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "DefenseUp")
        {
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "SpeedUp")
        {
            other.gameObject.SetActive(false);
        }


    }

    private void UseMana(int cost)
    {
        currentMana -= cost;
    }

    private void AddMana(int amount)
    {
        if (currentMana + amount < totalMana)
        {
            currentMana += amount;
        }
        else
        {
            currentMana = totalMana;
        }

    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
