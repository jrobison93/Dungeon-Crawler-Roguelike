using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.5f;
    public LayerMask blockingLayer;
    public int level = 1;
    public int baseHealth = 100;
    public float baseAttack = 10;
    public float moveSpeed = 0.25f;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    protected StatisticInterface health;

    protected float startTime = Time.time;
    protected GameManager manager = GameManager.instance;

    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        
        if (hit.transform == null && manager.movingObjects.GetLength(0) > end.x && end.x >= 0 &&
            manager.movingObjects.GetLength(1) > end.y && end.y >= 0 && !manager.movingObjects[(int)end.x, (int)end.y])
        {
            manager.movingObjects[(int)end.x, (int)end.y] = true;
            manager.movingObjects[(int)start.x, (int)start.y] = false;
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
            
            rb2D.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health.ReduceValue(damage);
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}