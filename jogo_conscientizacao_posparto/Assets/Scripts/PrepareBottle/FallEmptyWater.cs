using UnityEngine;

public class FallEmptyWater : MonoBehaviour
{
    RectTransform rectTr;
    RectTransform bottleRect;

    void Start()
    {
        rectTr = GetComponent<RectTransform>();
        GameObject babyBottle = GameObject.FindGameObjectWithTag("BabyBottle");
        if (babyBottle != null)
        {
            bottleRect = babyBottle.GetComponent<RectTransform>();
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = rectTr.anchoredPosition;
        pos.y -= 5f;
        rectTr.anchoredPosition = pos;

        if (bottleRect != null && IsOverlapping(rectTr, bottleRect))
        {
            ScoreManager.Instance.SubtractScore(1);
            Destroy(gameObject);
            return;
        }

        if (pos.y < -700f)
        {
            Destroy(gameObject);
        }
    }

    bool IsOverlapping(RectTransform a, RectTransform b)
    {
        Vector2 aPos = a.anchoredPosition - Vector2.Scale(a.sizeDelta, a.pivot);
        Vector2 bPos = b.anchoredPosition - Vector2.Scale(b.sizeDelta, b.pivot);

        Rect rectA = new Rect(aPos, a.sizeDelta);
        Rect rectB = new Rect(bPos, b.sizeDelta);

        return rectA.Overlaps(rectB);
    }
}
