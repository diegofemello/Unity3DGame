using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

  public float fallingTime;

  private TargetJoint2D targetJoint;
  private BoxCollider2D boxCollider;


  // Start is called before the first frame update
  void Start()
  {
    targetJoint = GetComponent<TargetJoint2D>();
    boxCollider = GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Invoke("Falling", fallingTime);
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "GameOver")
    {
      Destroy(gameObject);
    }
  }


  void Falling(){
    targetJoint.enabled = false;
    boxCollider.isTrigger = true;
  }
}
