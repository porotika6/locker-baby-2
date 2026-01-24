using UnityEngine;

[RequireComponent(typeof(ClickableItem))]
public class ContainerItem : MonoBehaviour
{
    [SerializeField] private ClickableItem _item;
    [SerializeField] private ClickableItem[] _itemsInContainer;

    [Header("Visuals")]
    [SerializeField] private HoverableItem _hover;
    [SerializeField] private Sprite _openedSprite;
    [SerializeField] private Sprite _openedHoverSprite;

    private bool _isOpened;
    private Sprite _closedSprite;
    private Sprite _closedHoverSprite;

    private void Reset()
    {
        _item = GetComponent<ClickableItem>();
        _hover = GetComponent<HoverableItem>();
    }

    private void Awake()
    {
        _closedSprite = _hover.SpriteRenderer.sprite;
        _closedHoverSprite = _hover.HoverSprite;

        if (!_openedSprite)
        {
            Debug.LogWarning($"<color=yellow>[{name}]</color> Opened Sprite is not assigned. Assigning the closed sprite as the opened sprite.", this);
            _openedSprite = _closedSprite;
        }

        if (!_openedHoverSprite)
        {
            Debug.LogWarning($"<color=yellow>[{name}]</color> Opened Hover Sprite is not assigned. Assigning the closed hover sprite as the opened hover sprite.", this);
            _openedHoverSprite = _closedHoverSprite;
        }
    }

    private void Start()
    {
        foreach (var item in _itemsInContainer)
        {
            if (!item)
                continue;

            var itemPos = item.transform.position;
            itemPos.z -= 0.1f;
            item.transform.position = itemPos;
            item.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _item.Clicked.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _item.Clicked.RemoveListener(OnClicked);
    }

    public void OpenContainer()
    {
        if (_isOpened)
            return;

        foreach (var item in _itemsInContainer)
        {
            if (item)
                item.gameObject.SetActive(true);
        }

        _hover.SetDefaultSprite(_openedSprite);
        _hover.SetHoverSprite(_openedHoverSprite);
        _isOpened = true;
    }

    public void CloseContainer()
    {
        if (!_isOpened)
            return;

        foreach (var item in _itemsInContainer)
        {
            if (item)
                item.gameObject.SetActive(false);
        }
        
        _hover.SetDefaultSprite(_closedSprite);
        _hover.SetHoverSprite(_closedHoverSprite);
        _isOpened = false;
    }

    private void OnClicked()
    {
        if (_isOpened)
            CloseContainer();
        else
            OpenContainer();
    }
}
