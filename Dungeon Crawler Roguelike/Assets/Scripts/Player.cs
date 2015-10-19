using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MovingObject
{
    public int baseMana;
    public Slider healthSlider;
    public Slider manaSlider;

    private float walkTime = 0.25f;
    private float startTime = Time.time;
    private float totalMana;
    private float currentMana;
    private float attackMod = 1f;
    private float manaMod = 1f;
    private float speedMod = 1f;
    private float defenseMod = 1f;

    protected override void Start()
    {
        base.Start();
        GameObject health = GameObject.FindGameObjectWithTag("HealthSlider");
        healthSlider = health.GetComponent<Slider>();

        GameObject mana = GameObject.FindGameObjectWithTag("ManaSlider");
        manaSlider = mana.GetComponent<Slider>();

        totalMana = baseMana;
        currentMana = totalMana;
    }

    // Update is called once per frame
    void Update ()
    {
        if (startTime + walkTime - Mathf.Log(speedMod) <= Time.time)
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
            attackMod++;
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
            other.gameObject.SetActive(false);
        }


    }

    private void ApplyManaMod()
    {
        int manaIncrease = (int)Math.Ceiling(Math.Log(manaMod, 2));
        totalMana += manaIncrease;
        currentMana += manaIncrease;

        manaSlider.value = (currentMana / totalMana);
    }

    private void ApplyDefenseMod()
    {
        int defenseIncrease = (int)Math.Ceiling(Math.Log(defenseMod, 2));
        totalHealth += defenseIncrease;
        currentHealth += defenseIncrease;

        healthSlider.value = (currentHealth / totalHealth);
    }

    private void UseMana(int cost)
    {
        currentMana -= cost;
        manaSlider.value = (currentMana / totalMana);
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

        manaSlider.value = (currentMana / totalMana);

    }

    public void AddHealth(int amount)
    {
        if (currentHealth + amount < totalHealth)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = totalHealth;
        }

        healthSlider.value = (currentHealth / totalHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = (currentHealth / totalHealth);
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
