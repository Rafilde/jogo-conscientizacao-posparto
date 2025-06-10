using UnityEngine;

public class FallFullWater : MonoBehaviour
{
    RectTransform rectTr;

    void Start()
    {
        rectTr = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        Vector2 pos = rectTr.anchoredPosition;
        pos.y -= 5f; 
        rectTr.anchoredPosition = pos;

        if (pos.y < -700f) 
        {
            Destroy(gameObject);
        }
    }
}
