using UnityEngine;

// Script para objetos de sujeira/fralda suja
public class Cleanable : MonoBehaviour
{
    [Tooltip("Quantidade de 'scrub' necessária para limpar totalmente.")]
    public float scrubThreshold = 100f;
    [Tooltip("Quanto de progresso (0–1) este objeto dá ao ser limpo.")]
    public float progressOnClean = 0.1f;

    private float scrubAmount = 0f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private UIController uiController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        uiController = FindObjectOfType<UIController>();
    }

    // Chamado pelo pano quando estiver em contato
    public void Scrub(float amount)
    {
        scrubAmount += amount;
        float percentCleaned = Mathf.Clamp01(scrubAmount / scrubThreshold);

        // Ajusta opacidade conforme limpeza
        Color newColor = originalColor;
        newColor.a = Mathf.Lerp(originalColor.a, 0f, percentCleaned);
        spriteRenderer.color = newColor;

        if (scrubAmount >= scrubThreshold)
        {
            uiController.AddProgress(progressOnClean);
            Destroy(gameObject);
        }
    }
}