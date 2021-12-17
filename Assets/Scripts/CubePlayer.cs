using Cinemachine;
using UnityEngine;

public class CubePlayer : MonoBehaviour
{
    private Rigidbody rb;

    public float forceMultiplier;

    public float maxVelocity;

    public float jumpForce;

    public ParticleSystem deathParticles;

    private CinemachineImpulseSource cinemachineImpulseSource;

    public GameObject mainVCam;

    public GameObject zoomVCam;

    public bool isJumping = false;

    public bool doubleJump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (rb.velocity.magnitude <= maxVelocity)
        {
            rb
                .AddForce(new Vector3(horizontalInput, 0, verticalInput) *
                forceMultiplier * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            // anim.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") > 0f)
        {
            // anim.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            // anim.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                doubleJump = true;
                isJumping = true;
            }
            else if (doubleJump)
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                doubleJump = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Instantiate(deathParticles,
            transform.position,
            Quaternion.identity);
            Destroy (gameObject);

            cinemachineImpulseSource.GenerateImpulse();

            mainVCam.SetActive(false);
            zoomVCam.SetActive(true);
        }

        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
