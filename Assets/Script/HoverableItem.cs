using UnityEngine;
using UnityEngine.InputSystem;

public class DetectHover : MonoBehaviour, IHoverable
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

    public void OnHoverEntered()
    {
        isHovered = true;

        if (SpriteRenderer != null && HoverSprite != null)
            SpriteRenderer.sprite = HoverSprite;
    }

    public void OnHoverExited()
    {
        isHovered = false;

        if (SpriteRenderer != null)
            SpriteRenderer.sprite = defaultSprite;
    }

    /// <summary>
    /// Reset is called when the user hits the Reset button in the
    /// Inspector's context menu OR when adding the component the first time.
    /// </summary>
    private void Reset()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
