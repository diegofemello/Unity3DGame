using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;

    public float forceMultiplier;

    public float maxVelocity;

    private Rigidbody rb;

    private Animator anim;

    public bool isJumping = false;

    public bool doubleJump = false;

    private bool isBlowing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);        

        if (rb.velocity.magnitude <= maxVelocity)
        {
            rb.AddForce(movement * forceMultiplier * Time.deltaTime * maxVelocity);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBlowing)
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
                doubleJump = true;
                anim.SetBool("isJumping", true);
            }
            else if (doubleJump)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
                doubleJump = false;
                anim.SetBool("doubleJump", true);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fan")
        {
            isBlowing = true;
            anim.SetBool("isBlowing", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fan")
        {
            isBlowing = false;
            anim.SetBool("isBlowing", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
            anim.SetBool("doubleJump", false);
        }

        if (collision.gameObject.tag == "GameOver")
        {
            Destroy (gameObject);
            // GameManager.instance.ShowGameOver();
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }
}
