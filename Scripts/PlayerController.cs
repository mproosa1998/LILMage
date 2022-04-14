using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Public
    public Vector2 speed = new Vector2(2,2);
    public Animator animator;
    public bool flipX;
    public SpriteRenderer _renderer;

    // Private
    private Rigidbody2D rigb;

    // Assign Rigidbody2D on Initialization
    private void Start()
    {
        rigb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Takes input from Player
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Convert input to Movement
        Vector2 movement = new Vector2(inputX * speed.x, inputY * speed.y);
        movement *= speed * Time.deltaTime;
        rigb.MovePosition(rigb.position + movement);
        animator.SetFloat("speed", Mathf.Abs(movement.x));

        // Detect Mouse Position in World
        var mouse = Input.mousePosition;
        mouse.z = 0f;
        Vector3 difference = Camera.main.ScreenToWorldPoint(mouse) - transform.position;
        difference.Normalize();

        // If mouse is on the opposite side of Player flip Sprite
        if(difference.x > 0)
        {
            _renderer.flipY = false;
        }
        if (difference.x < 0)
        {
            _renderer.flipY = true;
        }

        // Rotate towards Mouse
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }
}
