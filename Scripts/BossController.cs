using System.Collections;
using UnityEngine;
public class BossController : MonoBehaviour
{
    // Public
    public Animator animator;
    public GameObject Player;
    public SpriteRenderer _renderer;

    // Private
    private Transform target;
    private Rigidbody2D rb;
    private Vector3 dir;
    private bool isAlive = true;
    private new Collider2D collider2D;

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
        // If Player exists and gameObject.isAlive
        if (Player && isAlive)
        {
            // Check if Sprite needs to be flipped or Rotated
            FlipSprite();
            RotateTowardsTarget();
        }
    }

    // Get reletive Position from Player.target and flip sprit if needed
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

    // Obtain Vector relation between Player.target and Self -> Rotate towards Player
    void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    // Called from PlayerBullet upon Collision with Boss Collider
    public void Dead()
    {
        // Destroy Children so only Death Anim Remains
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // Disable Collider and Movement
        collider2D.enabled = false;
        isAlive = false;
        rb.velocity = new Vector2(0, 0);

        // Call DeathAnim IEnumerator
        StartCoroutine(DeathAnim());
    }

    // Play Death Animation after Resetting Orientation -> Destroy self
    IEnumerator DeathAnim()
    {
        _renderer.flipY = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Object.Destroy(this.gameObject);
    }
}