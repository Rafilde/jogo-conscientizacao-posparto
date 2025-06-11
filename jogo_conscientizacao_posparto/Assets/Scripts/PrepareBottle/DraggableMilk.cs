using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableMilk : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("1!");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("2!");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("3!");
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetPosition()
    {
        Debug.Log("4!");
        rectTransform.anchoredPosition = startPosition;
    }
}

