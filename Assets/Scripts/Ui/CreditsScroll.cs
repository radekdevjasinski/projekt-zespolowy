using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsScroll : MonoBehaviour, IPointerClickHandler
{
    public float scrollSpeed = 30f;
    private RectTransform rectTransform;
    private float textHeight;
    private Vector2 startPosition;
    private Vector2 resetPosition;
    private bool creditsActive = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textHeight = rectTransform.rect.height;
        startPosition = rectTransform.anchoredPosition;
        resetPosition = new Vector2(startPosition.x, startPosition.y + textHeight);
    }

    void Update()
    {
        if (creditsActive)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            if (rectTransform.anchoredPosition.y >= resetPosition.y)
            {
                rectTransform.anchoredPosition = startPosition;
            }
        }
    }

    public void ToggleCredits()
    {
        creditsActive = !creditsActive;
        gameObject.SetActive(creditsActive);

        if (creditsActive)
        {
            rectTransform.anchoredPosition = startPosition;
        }
        else
        {
            rectTransform.anchoredPosition = resetPosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleCredits();
    }
}
