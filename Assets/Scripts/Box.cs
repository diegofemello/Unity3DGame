using UnityEngine;

public class Box : MonoBehaviour
{
    public float jumpForce;
    public bool isUp;

    public int health = 5;

    public Animator anim;
    public GameObject explosion;

    void Update()
    {
        if (health <= 0)
        {
           Instantiate(explosion, transform.position, transform.rotation);
           Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player")
    {
        if (isUp)
        {
            anim.SetTrigger("hit");
            health--;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);
        }
        else
        {
            anim.SetTrigger("hit");
            health--;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0f, -jumpForce), ForceMode.Impulse);
        }
    }
  }
}
