using UnityEngine;

public class Trampoline : MonoBehaviour
{
  private Animator anim;

  void Start()
  {
    anim = GetComponent<Animator>();
  }
  
  public float bounceForce = 10f;
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Debug.Log("Trampoline");
      collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0, bounceForce), ForceMode.Impulse);

      anim.SetTrigger("bounce");
    }
  }
}
