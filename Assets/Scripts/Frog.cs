using UnityEngine;

public class Frog : MonoBehaviour
{

    private Rigidbody rig;
    private Animator anim;

    public float speed;
    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    public bool colliding;
    public bool colliding2;

    public LayerMask layer;
    public LayerMask layer2;

    public BoxCollider boxCollider;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(
            rightCol.position,
            leftCol.position,
            layer
        );

        colliding2 = Physics2D.Linecast(
            rightCol.position,
            leftCol.position,
            layer2
        );

        if (colliding || colliding2)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);

            speed *= -1f;
        }
    }

    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - headPoint.position.y;

            Debug.Log(height);
            
            if (height > 0 && !playerDestroyed)
            {
                
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector2.up * 3, ForceMode.Impulse);

                speed = 0;
                anim.SetTrigger("die");
                boxCollider.enabled = false;
                boxCollider.enabled = false;

                // rig.bodyType = RigidbodyType2D.Kinematic;

                Destroy(gameObject, 0.35f);
            } 
            else
            {
                playerDestroyed = true;
                // GameManager.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
            

        }
    }
}
