using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MovingObject
{
    public int baseMana;
    public Slider healthSlider;
    public Slider manaSlider;
    public GameObject specialAbility;
    public float restartLevelDelay = 1f;
    public float specialCost;
    public float specialBaseDamage;

    private float attackMod = 1f;
    private float manaMod = 1f;
    private float speedMod = 1f;
    private float defenseMod = 1f;
    private float specialMod = 1f;

    private StatisticInterface mana;

    protected override void Start()
    {
        base.Start();
        SetupSliders();

        health = new PlayerHealth(baseHealth);
        mana = new PlayerMana(baseMana);

        DontDestroyOnLoad(gameObject);
    }

    private void SetupSliders()
    {

        GameObject health = GameObject.FindGameObjectWithTag("HealthSlider");
        healthSlider = health.GetComponent<Slider>();

        GameObject mana = GameObject.FindGameObjectWithTag("ManaSlider");
        manaSlider = mana.GetComponent<Slider>();
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
                Special special = (Instantiate(specialAbility, new Vector3(transform.position.x + specialHorizontal, transform.position.y + specialVertical, 0f), Quaternion.identity) as GameObject).GetComponent<Special>();

                special.xDir = specialHorizontal;
                special.yDir = specialVertical;
                special.baseAttack = specialBaseDamage;
                special.SetUpSprite();

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

        manaSlider.value = mana.Percentage();
    }

    private void ApplyDefenseMod()
    {
        health.IncreaseTotal(defenseMod);

        healthSlider.value = health.Percentage();
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
        float specialIncrease = (float)Math.Log(specialMod, 10);
        specialBaseDamage += specialIncrease;
    }

    private void UseMana(float cost)
    {
        mana.ReduceValue(cost);
        manaSlider.value = mana.Percentage();
    }

    private void AddMana(float amount)
    {
        mana.AddValue(amount);

        manaSlider.value = mana.Percentage();

    }

    public void AddHealth(float amount)
    {
        health.AddValue(amount);

        healthSlider.value = health.Percentage();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = health.Percentage();

        if (health.IsDepleted())
        {
            GameManager.instance.GameOver();
        }
    }

    public void LevelUp()
    {
        level++;
        SetupSliders();
        healthSlider.value = health.Percentage();
        manaSlider.value = mana.Percentage();
        enabled = true;
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
