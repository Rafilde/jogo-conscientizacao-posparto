using UnityEngine;

public class MoveBabyBottle : MonoBehaviour
{
    RectTransform rectTr;

    void Start()
    {
        rectTr = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        Vector2 pos = rectTr.anchoredPosition;

        if (Input.GetKey("right") && pos.x < 251f) 
        {
            pos.x += 10f;
        }

        if (Input.GetKey("left") && pos.x > -249f)
        {
            pos.x -= 10f;
        }

        rectTr.anchoredPosition = pos;
    }
}
