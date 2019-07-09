using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private bool isGrounded;

    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float jumpForce = 1500.0f;

    [SerializeField]
    private GameObject feet;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float groundCheckRange = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento horizontal 
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0.0f)
        {
            Vector2 velocity = new Vector2(speed * horizontal * Time.deltaTime, rb.velocity.y);
            rb.velocity = velocity;

            if (isGrounded)
            {
                animator.SetBool("Walking", true);
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (horizontal < 0.0f)
        {
            sprite.flipX = true;
        }
        else if (horizontal >= 0.0f)
        {
            sprite.flipX = false;
        }

        // Salto

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", isGrounded);
            rb.AddForce(new Vector2(0.0f, 1.0f * jumpForce));
            isGrounded = false;
        }


        isGrounded = CheckIsGrounded();
        animator.SetBool("Grounded", isGrounded);

    }

    private bool CheckIsGrounded()
    {
        return Physics2D.OverlapCircle(feet.transform.position, groundCheckRange, groundMask);
    }
}
