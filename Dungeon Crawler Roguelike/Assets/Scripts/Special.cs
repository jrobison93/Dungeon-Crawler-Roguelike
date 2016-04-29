using UnityEngine;
using System.Collections;
using System;

public abstract class Special : MovingObject
{
    public int xDir;
    public int yDir;
    public Sprite[] sprites;
    public float modifier;

    protected String specialAbilityPath;
    private SpriteRenderer sprite;

    public virtual void Cast(Vector3 origin, Vector3 direction, float specialMod)
    {
        Special special = InstantiateSpecial(origin, direction);

        special.xDir = (int)direction.x;
        special.yDir = (int)direction.y;
        special.modifier = specialMod;
        special.SetUpSprite();

    }

    Special InstantiateSpecial(Vector3 origin, Vector3 direction)
    {
        Debug.Log(specialAbilityPath + " is the path");
        Debug.Log(this.GetType());
        return (Instantiate(Resources.Load(specialAbilityPath), origin + direction, Quaternion.identity) as GameObject).GetComponent<Special>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPaused && startTime + moveSpeed <= Time.time)
        {
            AttemptMove<MovingObject>(xDir, yDir);
        }
    }

    public void SetUpSprite()
    {
        sprite = GetComponent<SpriteRenderer>();
        startTime = Time.time - (moveSpeed / 2);
        manager.movingObjects[(int)transform.position.x, (int)transform.position.y] = true;
        if (xDir == 1)
        {
            sprite.sprite = sprites[0];
        }
        else if (xDir == -1)
        {
            sprite.sprite = sprites[1];
        }
        else if(yDir == 1)
        {
            sprite.sprite = sprites[2];
        }
        else if(yDir == -1)
        {
            sprite.sprite = sprites[3];
        }

    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;
        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove)
        {
            if (hitComponent != null)
            {
                OnCantMove(hitComponent);
            }

            manager.movingObjects[(int)transform.position.x, (int)transform.position.y] = false;
            enabled = false;
            Destroy(gameObject);

        }
    }

    public override void TakeDamage(float damage)
    {
        manager.movingObjects[(int)transform.position.x, (int)transform.position.y] = false;
        enabled = false;
        Destroy(gameObject);

    }

    protected override void OnCantMove<T>(T component)
    {
        MovingObject enemyHit = component as MovingObject;
        enemyHit.TakeDamage(baseAttack + modifier);
    }
}
