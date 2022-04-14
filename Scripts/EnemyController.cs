using System.Collections;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    // Public
    public float speed;
    public float chaseRange;
    public bool shouldRotate;
    public LayerMask whatIsPlayer;
    public Animator animator;
    public SpriteRenderer _renderer;
    public GameObject Player;

    // Private
    private float distance;
    private bool canMove = true;
    private new Collider2D collider2D;
    private Transform target;
    private Rigidbody2D rb;
    private Vector3 dir;

    void Start()
    {
        // Initialize Game Elements
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        target = Player.transform;
        collider2D = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        // If Player exists and gameObject can Move
        if (Player && canMove)
        {
            // If Player is within Range -> Move, Rotate, and Flip
            distance = Vector3.Distance(gameObject.transform.position, target.transform.position); ;
            if (distance < chaseRange)
            {
                FlipSprite();
                MoveCharacter(dir);
                RotateTowardsTarget();
            }
        }
    }

    // Get reletive Position from Player.target and flip Sprit if needed
    void FlipSprite()
    {
        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();

        // Flip sprite along Y axis if Player is to the Left of gameObject
        if (dir.x > 0)
            _renderer.flipY = false;

        if (dir.x < 0)
            _renderer.flipY = true;
    }

    // Move toward Player and play Move Animation
    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
        animator.SetFloat("speed", Mathf.Abs(speed));
    }

    // Obtain Vector relation between Player.target and Self -> Rotate towards Player
    void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    // Called from PlayerBullet upon Collision with Enemy Collider
    public void Dead()
    {
        // Destroy Children so only Death Anim Remains
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // Disable Collider and Movement
        collider2D.enabled = false;
        canMove = false;
        rb.velocity = new Vector2(0, 0);

        // Call DeathAnim IEnumerator
        StartCoroutine(DeathAnim());
    }

    // Play Death Animation after Resetting Orientation -> Destroy self
    public IEnumerator DeathAnim()
    {
        _renderer.flipY = false;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));

        animator.SetBool("isShooting", false);
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1.5f);
        Object.Destroy(this.gameObject);
    }
}