using UnityEngine;
using System.Collections;
using System;

public class Special : MovingObject
{
    public int xDir;
    public int yDir;
    public Sprite[] sprites;

    private SpriteRenderer sprite;

    // Update is called once per frame
    void Update()
    {
        if (startTime + moveSpeed <= Time.time)
        {
            AttemptMove<Enemy>(xDir, yDir);
        }
    }

    public void SetUpSprite()
    {
        sprite = GetComponent<SpriteRenderer>();
        startTime = Time.time - (moveSpeed / 2);
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

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }

        if(hitComponent != null ||hit.transform.tag == "Wall")
        {
            enabled = false;
            Destroy(gameObject);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Enemy enemyHit = component as Enemy;
        enemyHit.TakeDamage(baseAttack);
    }
}
