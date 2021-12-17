using UnityEngine;

public class Apple : MonoBehaviour
{
    private SpriteRenderer sr;private BoxCollider circle;
    public GameObject collected;
    public int score;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<BoxCollider>();
        collected.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            GameManager.instance.totalScore += score;
            GameManager.instance.UpdateScoreText();

            Destroy(gameObject, 0.5f);
        }
    }
}
