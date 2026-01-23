using UnityEngine;
using UnityEngine.Assertions;

public class HoverableItem : MonoBehaviour, IHoverable
{
    public SpriteRenderer SpriteRenderer;
    public Sprite HoverSprite;

    private Sprite _defaultSprite;
    private bool _isHovered;

    void Awake()
    {
        Assert.IsNotNull(SpriteRenderer, $"{nameof(SpriteRenderer)} is not assigned in {gameObject.name}.");
        _defaultSprite = SpriteRenderer.sprite;
    }

    // Called by HoverManager when the mouse starts hovering over the object.
    public void OnHoverEntered()
    {
        _isHovered = true;

        if (HoverSprite != null)
            SpriteRenderer.sprite = HoverSprite;
    }

    // Called by HoverManager when the mouse stops hovering over the object.
    public void OnHoverExited()
    {
        _isHovered = false;
        SpriteRenderer.sprite = _defaultSprite;
    }

    public void SetDefaultSprite(Sprite sprite)
    {
        _defaultSprite = sprite;

        if (!_isHovered)
            SpriteRenderer.sprite = _defaultSprite;
    }

    public void SetHoverSprite(Sprite sprite)
    {
        HoverSprite = sprite;

        if (_isHovered && HoverSprite != null)
            SpriteRenderer.sprite = HoverSprite;
    }

    // Reset is called when the user hits the Reset button in the
    // Inspector's context menu OR when adding the component the first time.
    private void Reset()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
