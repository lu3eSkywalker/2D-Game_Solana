using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    private bool isGameOver = false;
    private int score = 0;

    void Start()
    {
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (scoreText != null) scoreText.text = "0";
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            if (gameOverText != null) gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void AddScore(int amount)
    {
        if (!isGameOver)
        {
            score += amount;
            if (scoreText != null)
                scoreText.text = score.ToString();
        }
    }
}
