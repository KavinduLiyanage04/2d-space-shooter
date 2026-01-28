using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float scrollSpeed = 10f;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // Optional loop reset (teleport back if too far left)
        if (rectTransform.anchoredPosition.x <= -rectTransform.rect.width)
        {
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
        }
    }
}
