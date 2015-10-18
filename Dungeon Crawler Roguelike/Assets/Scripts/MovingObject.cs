using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.25f;
    public LayerMask blockingLayer;
    public int level = 1;
    public int baseHealth = 100;
    public int baseAttack = 10;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private int totalHealth;
    private int currentHealth;

    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

        totalHealth = baseHealth;
        currentHealth = totalHealth;
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;
        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Taking damage: " + damage + " " + currentHealth);
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
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

    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}