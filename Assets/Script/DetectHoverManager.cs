using UnityEngine;
using UnityEngine.InputSystem;


public class DetectHovrManager : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isHovered;
    private Camera cam;
    private Sprite defaultSprite;
    public Sprite hoverSprite;
    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) defaultSprite = sr.sprite;
    }

    
    // Update is called once per frame
    void Update()
    {
         DetectHover();
        
    }

    void DetectHover()
    {
        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (!isHovered)
            {
                isHovered = true;
                  if (hoverSprite != null) sr.sprite = hoverSprite;
            }
        }
        else if (isHovered)
        {
            isHovered = false;
              if (sr != null) sr.sprite = defaultSprite;
        }
    }

    
}
