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
            Vector3 velocity = new Vector3(speed * horizontal * Time.deltaTime, 0.0f, 0.0f);
            rb.MovePosition(transform.position + velocity);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (horizontal < 0.0f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", isGrounded);
            rb.AddForce(new Vector2(0.0f, 1.0f * jumpForce));
        }


        isGrounded = CheckIsGrounded();
        animator.SetBool("Grounded", isGrounded);

    }

    private bool CheckIsGrounded()
    {
        return Physics2D.CircleCast(feet.transform.position, groundCheckRange, Vector2.down, 0.0f, groundMask);
    }
}
