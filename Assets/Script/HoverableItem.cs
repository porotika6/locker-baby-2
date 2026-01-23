using UnityEngine;

public class HoverableItem : MonoBehaviour, IHoverable
{
    public SpriteRenderer SpriteRenderer;
    public Sprite HoverSprite;

    private Sprite defaultSprite;
    private bool isHovered;

    void Start()
    {
        if (SpriteRenderer != null)
            defaultSprite = SpriteRenderer.sprite;
    }

    // Called by HoverManager when the mouse starts hovering over the object.
    public void OnHoverEntered()
    {
        isHovered = true;

        if (SpriteRenderer != null && HoverSprite != null)
            SpriteRenderer.sprite = HoverSprite;
    }

    // Called by HoverManager when the mouse stops hovering over the object.
    public void OnHoverExited()
    {
        isHovered = false;

        if (SpriteRenderer != null)
            SpriteRenderer.sprite = defaultSprite;
    }

    // Reset is called when the user hits the Reset button in the
    // Inspector's context menu OR when adding the component the first time.
    private void Reset()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
