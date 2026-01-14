using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Oyun İçi Skor")]
    public TMP_Text scoreText;       // Oyun sırasında gösterilecek skor
    public AudioSource audioSource;
    public AudioClip pointClip;

    [Header("Game Over Paneli")]
    public GameObject gameOverUI;    // Oyun bitince aktif olacak panel
    public TMP_Text currentScoreText;  
    public TMP_Text bestScoreText;     

    private int score = 0;
    private int bestScore = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if(scoreText != null)
            scoreText.gameObject.SetActive(false);

        if(gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void StartGame()
    {
        score = 0;

        if(scoreText != null)
        {
            scoreText.text = score.ToString();
            scoreText.gameObject.SetActive(true);
        }

        if(gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;

        if(scoreText != null)
            scoreText.text = score.ToString();

        if(audioSource != null && pointClip != null)
            audioSource.PlayOneShot(pointClip);
    }

    public void GameOver()
    {
        // Oyun içi skor gizle
        if(scoreText != null)
            scoreText.gameObject.SetActive(false);

        // GameOver UI aç
        if(gameOverUI != null)
            gameOverUI.SetActive(true);

        // Current ve Best score güncelle
        if(currentScoreText != null)
            currentScoreText.text = "Score: " + score.ToString();

        if(score > bestScore)
            bestScore = score;

        if(bestScoreText != null)
            bestScoreText.text = "Best Score: " + bestScore.ToString();
    }
}
