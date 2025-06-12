using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider progressSlider;
    public TMP_Text timerText;
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    [Header("Configurações")]
    public float totalTime = 60f;

    private float currentTime;
    private float progress = 0f;
    private bool gameEnded = false;

    void Start()
    {
        currentTime = totalTime;
        UpdateUI();
        if (victoryPanel) victoryPanel.SetActive(false);
        if (defeatPanel) defeatPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 0f;
            EndGame(false); // derrota
        }

        UpdateUI();

        if (progress >= 1f && !gameEnded)
        {
            EndGame(true); // vitória
        }
    }

    public void AddProgress(float amount)
    {
        if (gameEnded) return;

        progress += amount;
        progress = Mathf.Clamp01(progress);
        progressSlider.value = progress;

        if (progress >= 1f)
        {
            EndGame(true);
        }
    }

    void EndGame(bool win)
    {
        gameEnded = true;

        if (win && victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else if (!win && defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }

        // (Opcional: pode pausar o jogo)
        // Time.timeScale = 0f;
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(currentTime);
            timerText.text = seconds.ToString("00");
        }

        if (progressSlider != null)
        {
            progressSlider.value = progress;
        }
    }

    
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
