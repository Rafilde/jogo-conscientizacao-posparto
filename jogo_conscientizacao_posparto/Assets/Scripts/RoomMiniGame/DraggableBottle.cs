using UnityEngine;

public class DraggableBottle : MonoBehaviour
{
    [Tooltip("Quanto de progresso (0–1) esta ação dá ao ser concluída.")]
    public float progressOnPlace = 0.1f;

    private Vector3 offset;
    private Vector3 originalPosition;
    private bool placed = false;
    private Camera mainCam;
    private UIController uiController;

    void Start()
    {
        mainCam = Camera.main;
        uiController = FindObjectOfType<UIController>();
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (placed) return;
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
    }

    void OnMouseDrag()
    {
        if (placed) return;
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z) + offset;
    }

    void OnMouseUp()
    {
        if (placed) return;

        // Verifica se soltou dentro do BottleSpot
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("BottleSpot"))
            {
                // Snap na posição exata do BottleSpot
                transform.position = hit.transform.position;
                placed = true;
                // Opcional: desative collider/Rigidbody para travar
                if (TryGetComponent<Rigidbody2D>(out var rb)) rb.bodyType = RigidbodyType2D.Static;
                if (TryGetComponent<Collider2D>(out var col)) col.enabled = false;
                // Atualiza o progresso
                uiController.AddProgress(progressOnPlace);
                return;
            }
        }

        // Se não estiver no spot, volta à posição original
        transform.position = originalPosition;
    }
}
