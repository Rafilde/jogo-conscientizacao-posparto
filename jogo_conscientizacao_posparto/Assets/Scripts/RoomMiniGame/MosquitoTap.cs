using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MosquitoTap : MonoBehaviour
{
    [Header("Prefab e UI")]
    public GameObject hitPanelPrefab;    // Prefab do MosquitoHitPanel
    [Header("Configuração do timing")]
    public float progressOnKill = 0.1f;
    public float indicatorSpeed = 200f;

    private UIController uiController;
    private GameObject panelInstance;
    private RectTransform barBackground;
    private RectTransform indicator;
    private RectTransform targetZone;

    private bool isTimingActive = false;
    private bool movingRight = true;
    private float halfBarWidth;
    private float halfIndicatorWidth;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        if (uiController == null)
            Debug.LogError("MosquitoTap: não encontrou nenhum UIController na cena!");
    }

    void OnMouseDown()
    {
        if (hitPanelPrefab == null)
        {
            Debug.LogError("MosquitoTap: hitPanelPrefab NÃO está atribuído no Inspector!");
            return;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("MosquitoTap: não encontrou nenhum Canvas na cena!");
            return;
        }

        // Instancia o painel, mantendo a configuração de RectTransforms do prefab
        panelInstance = Instantiate(hitPanelPrefab, canvas.transform, false);
        panelInstance.transform.SetAsLastSibling(); // garante que fique acima de outros elementos

        Debug.Log("MosquitoTap: Painel instanciado: " + panelInstance.name);

        // DEBUG: informações do RectTransform root
        var rootRT = panelInstance.GetComponent<RectTransform>();
        Debug.Log($"[Debug RT] anchorMin: {rootRT.anchorMin}, anchorMax: {rootRT.anchorMax}, " +
                  $"anchoredPos: {rootRT.anchoredPosition}, sizeDelta: {rootRT.sizeDelta}");

        // Busca robusta pelos filhos
        var allRects = panelInstance.GetComponentsInChildren<RectTransform>(true);
        barBackground = allRects.FirstOrDefault(rt => rt.name == "BarBackground");
        indicator    = allRects.FirstOrDefault(rt => rt.name == "Indicator");
        targetZone   = allRects.FirstOrDefault(rt => rt.name == "TargetZone");

        if (barBackground == null) Debug.LogError("MosquitoTap: BarBackground não encontrado!");
        if (indicator    == null) Debug.LogError("MosquitoTap: Indicator não encontrado!");
        if (targetZone   == null) Debug.LogError("MosquitoTap: TargetZone não encontrado!");

        if (barBackground != null && indicator != null && targetZone != null)
        {
            // Posiciona o indicador no canto esquerdo da barra
            halfBarWidth       = barBackground.rect.width  * 0.5f;
            halfIndicatorWidth = indicator.rect.width      * 0.5f;
            indicator.anchoredPosition = new Vector2(-halfBarWidth + halfIndicatorWidth, 0);

            isTimingActive = true;
            movingRight    = true;
        }
    }

    void Update()
    {
        if (!isTimingActive || panelInstance == null) return;

        // Movimenta o indicador
        Vector2 pos = indicator.anchoredPosition;
        float step = indicatorSpeed * Time.deltaTime * (movingRight ? 1f : -1f);
        pos.x += step;

        float leftLimit  = -halfBarWidth + halfIndicatorWidth;
        float rightLimit =  halfBarWidth - halfIndicatorWidth;
        if (pos.x >= rightLimit) { pos.x = rightLimit; movingRight = false; }
        else if (pos.x <= leftLimit) { pos.x = leftLimit; movingRight = true; }

        indicator.anchoredPosition = pos;

        // Clique para tentar matar
        if (Input.GetMouseButtonDown(0))
        {
            isTimingActive = false;
            float hitX    = indicator.anchoredPosition.x;
            float zoneHalf = targetZone.rect.width * 0.5f;
            bool hit      = (hitX >= -zoneHalf && hitX <= zoneHalf);

            if (hit)
            {
                uiController.AddProgress(progressOnKill);
                Destroy(gameObject);
            }

            Destroy(panelInstance);
        }
    }
}
