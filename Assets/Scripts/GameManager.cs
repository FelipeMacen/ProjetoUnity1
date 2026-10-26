using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    public static GameManager Instance {get; private set;}

    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private int totemPoints = 100;

    private int score;

    private bool gameIsOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;
        gameIsOver = false;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (gameIsOver)
        {
            return;
        }

        gameIsOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RegisterTotemHit()
    {
        AddPoints(totemPoints);
    } 

    private void AddPoints(int amount)
    {
        if (gameIsOver)
        {
            return;
        }

        score += amount;

        UpdateScoreText();

        Debug.Log("Pontuação atual" + score);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Pontuação: {score}";
        }
    }

}
