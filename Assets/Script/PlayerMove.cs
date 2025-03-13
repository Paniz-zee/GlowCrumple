using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animation states
        if (movement.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Flip character based on movement direction
        if (movement.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.velocity = movement.normalized * moveSpeed;
    }
}
