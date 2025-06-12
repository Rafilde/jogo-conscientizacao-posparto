using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimingMinigame : MonoBehaviour
{
    public static TimingMinigame Instance;

    [Header("UI References")]
    public CanvasGroup minigameUI;
    public RectTransform indicator;
    public RectTransform targetZone;
    public Button actionButton;
    public float moveSpeed = 300f;

    private Action onSuccess;
    private Action onFail;
    private bool running = false;
    private int direction = 1;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        minigameUI.alpha = 0;
        minigameUI.interactable = false;
        minigameUI.blocksRaycasts = false;
    }

    public void StartMinigame(Action success, Action fail)
    {
        onSuccess = success;
        onFail = fail;
        StartCoroutine(RunMinigame());
    }

    private IEnumerator RunMinigame()
    {
        // Show UI
        minigameUI.alpha = 1;
        minigameUI.interactable = true;
        minigameUI.blocksRaycasts = true;

        running = true;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(OnAction);

        // Loop indicator movement until clicked
        while (running)
        {
            // Move
            Vector2 pos = indicator.anchoredPosition;
            pos.x += direction * moveSpeed * Time.deltaTime;
            // Reverse at edges
            float halfWidth = ((RectTransform)indicator.parent).rect.width * 0.5f;
            if (pos.x > halfWidth) { pos.x = halfWidth; direction = -1; }
            else if (pos.x < -halfWidth) { pos.x = -halfWidth; direction = 1; }
            indicator.anchoredPosition = pos;

            yield return null;
        }
    }

    private void OnAction()
    {
        running = false;
        // Hide UI
        minigameUI.alpha = 0;
        minigameUI.interactable = false;
        minigameUI.blocksRaycasts = false;

        // Check if indicator within target
        float indX = indicator.anchoredPosition.x;
        float tzMin = targetZone.anchoredPosition.x - targetZone.rect.width * 0.5f;
        float tzMax = targetZone.anchoredPosition.x + targetZone.rect.width * 0.5f;
        if (indX >= tzMin && indX <= tzMax)
        {
            onSuccess?.Invoke();
        }
        else
        {
            onFail?.Invoke();
        }
    }
}
