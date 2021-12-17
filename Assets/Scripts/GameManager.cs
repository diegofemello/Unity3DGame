using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefab;

    public int maxHazardsToSpawn = 3;

    public int totalScore;

    public static GameManager instance;

    // public GameObject gameOver;
    public TMPro.TextMeshProUGUI scoreText;

    private float timer;

    private static bool gameOver;

    public Image backgroundMenu;

    void Start()
    {
        StartCoroutine(SpawnHazards());
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                backgroundMenu.gameObject.SetActive(false);
                StartCoroutine(ScaleTime(0f, 1f, 0.5f));
            }
            else
            {
                StartCoroutine(ScaleTime(Time.timeScale, 0f, 0.5f));
                backgroundMenu.gameObject.SetActive(true);
            }
        }

        if (gameOver) {
            backgroundMenu.gameObject.SetActive(true);
            return;
        }

        UpdateScoreText();
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private IEnumerator SpawnHazards()
    {
        var hazardToSpawn = Random.Range(1, maxHazardsToSpawn);

        for (int i = 0; i < hazardToSpawn; i++)
        {
            var x = Random.Range(-7f, 7f);
            var drag = Random.Range(0f, 2f);
            var hazard =
                Instantiate(hazardPrefab,
                new Vector3(x, 11, 0),
                Quaternion.identity);

            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        yield return new WaitForSeconds(1);

        yield return SpawnHazards();
    }

    public void UpdateScoreText()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            totalScore++;
            scoreText.text = totalScore.ToString();
            timer = 0;
        }
    }

    public static void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over");
    }

    public void RestartGame(string lvlName)
    {
        StartCoroutine(ScaleTime(Time.timeScale, 1f, 0f));
        SceneManager.LoadScene(lvlName);
        gameOver = false;
    }

    public void NextLevel(string lvlName)
    {
        Debug.Log("Next Level");
        SceneManager.LoadScene (lvlName);
    }
}
