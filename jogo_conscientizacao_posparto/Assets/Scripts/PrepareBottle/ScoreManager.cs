using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public Text scoreText;
    public int scoreToWin = 50;
    public string nextSceneName; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score >= scoreToWin)
        {
            PlayerPrefs.SetInt("FaseAtual", 2); 
            SceneManager.LoadScene("PrepareBottle");
        }
    }

    public void SubtractScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Pontos: " + score;
    }
}
