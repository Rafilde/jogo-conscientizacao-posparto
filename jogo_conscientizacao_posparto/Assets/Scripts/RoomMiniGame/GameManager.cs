using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject endGamePanel;
    public GameObject timeoutPanel;
    public Image endGameImage;

    [Header("Timer")]
    public TMP_Text timerText;        // Arraste aqui seu TimerText
    public float timeLimit = 60f; // duração em segundos

    private float timeRemaining;
    private bool isGameOver = false;

    void Awake() {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        // Setup inicial
        endGamePanel.SetActive(false);
        timeoutPanel.SetActive(false);

        timeRemaining = timeLimit;
    }

    void Update() {
        if (isGameOver) return;

        // Atualiza cronômetro
        timeRemaining -= Time.unscaledDeltaTime;
        int sec = Mathf.CeilToInt(timeRemaining);
        timerText.text = $"{sec}";

        if (timeRemaining <= 0f) {
            OnTimeUp();
        }
    }

    public void OnGameComplete() {
        if (isGameOver) return;
        isGameOver = true;
        StartCoroutine(ShowEndGame());
    }

    IEnumerator ShowEndGame() {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        yield return null;
    }

    private void OnTimeUp() {
        isGameOver = true;
        Time.timeScale = 0f;
        timeoutPanel.SetActive(true);
    }

    // Se quiser botões de restart
    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
