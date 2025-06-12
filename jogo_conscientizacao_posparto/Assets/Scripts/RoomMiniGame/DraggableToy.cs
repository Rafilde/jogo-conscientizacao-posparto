using UnityEngine;

public class DraggableToy : MonoBehaviour
{
    [Tooltip("Quanto de progresso (0–1) este brinquedo dá ao ser organizado.")]
    public float progressOnCollect = 0.1f;

    private Vector3 offset;
    private Vector3 originalPosition;
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
        // Calcula o deslocamento entre o clique e o pivot do objeto
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
    }

    void OnMouseDrag()
    {
        // Move o brinquedo junto com o mouse
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z) + offset;
    }

    void OnMouseUp()
    {
        // Verifica se soltou dentro do cesto
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("CestaDeBrinquedos"))
            {
                // Sucesso: soma progresso e remove o brinquedo
                uiController.AddProgress(progressOnCollect);
                Destroy(gameObject);
                return;
            }
        }

        // Se não estiver no cesto, retorna ao lugar original
        transform.position = originalPosition;
    }
}
