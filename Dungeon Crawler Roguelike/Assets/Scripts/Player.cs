using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MovingObject
{
    public int baseMana;
    public GameObject specialAbility;
    public float restartLevelDelay = 1f;
    public float specialCost;
    public float specialBaseDamage;
    public GameObject beginningSpecial;

    private float attackMod = 1f;
    private float manaMod = 1f;
    private float speedMod = 1f;
    private float defenseMod = 1f;
    private float specialMod = 1f;
    private float specialIncrease = 0.0f;
    private Special currentSpecial;

    private StatisticInterface mana;

    protected override void Start()
    {
        base.Start();

        currentSpecial = specialAbility.GetComponent<Special>();

        health = new PlayerHealth(baseHealth);
        mana = new PlayerMana(baseMana);

        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update ()
    {
        if (startTime + moveSpeed <= Time.time)
        {
            int horizontal = (int)Input.GetAxisRaw("Horizontal");
            int vertical = (int)Input.GetAxisRaw("Vertical");
            int specialHorizontal = (int)Input.GetAxisRaw("SpecialHorizontal");
            int specialVertical = (int)Input.GetAxisRaw("SpecialVertical");

            if (horizontal != 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove<Enemy>(horizontal, vertical);
            }

            if(specialHorizontal != 0)
            {
                specialVertical = 0;
            }

            if ((specialHorizontal != 0 || specialVertical != 0) && specialCost <= mana.CurrentValue()) 
            {
                currentSpecial.Cast(transform.position, new Vector3(specialHorizontal, specialVertical, 0), specialIncrease);

                UseMana(specialCost);

            }


            startTime = Time.time;
        }
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            GameManager.instance.levelUp = true;
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "HealthPotion")
        {
            AddHealth(other.GetComponent<Potion>().GetValue());
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ManaPotion")
        {
            AddMana(other.GetComponent<Potion>().GetValue());
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "AttackUp")
        {
            attackMod++;
            ApplyAttackMod();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "ManaUp")
        {
            manaMod++;
            ApplyManaMod();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "DefenseUp")
        {
            defenseMod++;
            ApplyDefenseMod();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "SpeedUp")
        {
            speedMod += 0.01f;
            ApplySpeedMod();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "SpecialUp")
        {
            specialMod++;
            ApplySpecialMod();
            other.gameObject.SetActive(false);
        }


    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void ApplyManaMod()
    {
        mana.IncreaseTotal(manaMod);
    }

    private void ApplyDefenseMod()
    {
        health.IncreaseTotal(defenseMod);
    }

    private void ApplySpeedMod()
    {
        float speedIncrease = (float)Math.Log(speedMod, 10);
        moveSpeed += speedIncrease;
    }

    private void ApplyAttackMod()
    {
        float attackIncrease = (float)Math.Log(attackMod, 10);
        baseAttack += attackIncrease;
    }

    private void ApplySpecialMod()
    {
        specialIncrease = (float)Math.Log(specialMod, 10);
    }

    private void UseMana(float cost)
    {
        mana.ReduceValue(cost);
    }

    private void AddMana(float amount)
    {
        mana.AddValue(amount);
    }

    public void AddHealth(float amount)
    {
        health.AddValue(amount);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (health.IsDepleted())
        {
            GameManager.instance.GameOver();
        }
    }

    public void LevelUp()
    {
        level++;
        health.NotifyObservers();
        mana.NotifyObservers();
        enabled = true;
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
