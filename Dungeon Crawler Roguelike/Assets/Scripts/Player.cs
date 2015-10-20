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

    private float totalMana;
    private float currentMana;
    private float attackMod = 1f;
    private float manaMod = 1f;
    private float speedMod = 1f;
    private float defenseMod = 1f;

    protected override void Start()
    {
        base.Start();
        SetupSliders();

        totalMana = baseMana;
        currentMana = totalMana;

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

            if ((specialHorizontal != 0 || specialVertical != 0) && specialCost <= currentMana) 
            {
                Special special = (Instantiate(specialAbility, new Vector3(transform.position.x + specialHorizontal, transform.position.y + specialVertical, 0f), Quaternion.identity) as GameObject).GetComponent<Special>();

                special.xDir = specialHorizontal;
                special.yDir = specialVertical;
                special.baseAttack = specialBaseDamage + Mathf.Log(attackMod, 10);
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


    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void ApplyManaMod()
    {
        float manaIncrease = (float)Math.Log(manaMod, 2);
        totalMana += manaIncrease;
        currentMana += manaIncrease;

        manaSlider.value = (currentMana / totalMana);
    }

    private void ApplyDefenseMod()
    {
        float defenseIncrease = (float)Math.Log(defenseMod, 2);
        totalHealth += defenseIncrease;
        currentHealth += defenseIncrease;

        healthSlider.value = (currentHealth / totalHealth);
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

    private void UseMana(float cost)
    {
        currentMana -= cost;
        manaSlider.value = (currentMana / totalMana);
    }

    private void AddMana(float amount)
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

    public void AddHealth(float amount)
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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = (currentHealth / totalHealth);

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public void LevelUp()
    {
        level++;
        SetupSliders();
        healthSlider.value = (currentHealth / totalHealth);
        manaSlider.value = (currentMana / totalMana);
        enabled = true;
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
