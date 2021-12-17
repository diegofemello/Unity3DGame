using UnityEngine;

public class NextLevelPoint : MonoBehaviour
{
    public string lvlName;

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Next Level");
            GameManager.instance.NextLevel(lvlName);
        }
    }
}
