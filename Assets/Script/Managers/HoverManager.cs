using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public interface IHoverable
{
    /// <summary>
    /// Determines whether the object can be hovered.
    /// </summary>
    public bool CanBeHovered() => true;

    /// <summary>
    /// Called when the mouse starts hovering over the object.
    /// </summary>
    public void OnHoverEntered();

    /// <summary>
    /// Called when the mouse stops hovering over the object.
    /// </summary>
    public void OnHoverExited();
}

public class HoverManager : MonoBehaviour
{
    public LayerMask ClickableLayers = -1; // -1 means all layers

    private const float ForwardDistanceFromCamera = 20f;

    private Camera _cam;
    private IHoverable _currentHoverable;

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        var hoverManagerObj = new GameObject("HoverManager");
        hoverManagerObj.AddComponent<HoverManager>();
        DontDestroyOnLoad(hoverManagerObj);
    }

    private void Awake()
    {
        Assert.IsNotNull(Camera.main, "Main Camera not found in the scene.");
        _cam = Camera.main;

        // Detect if there are multiple instances
        if (FindObjectsByType<HoverManager>(FindObjectsSortMode.None).Length > 1)
            Debug.LogError("Multiple HoverManager instances detected. There should only be one HoverManager in the scene.");
    }

    private void Update()
    {
        var pointer = Pointer.current;
        Ray ray = _cam.ScreenPointToRay(pointer.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, ForwardDistanceFromCamera, ClickableLayers);
        Collider2D other = hit.collider;
        if (other && other.TryGetComponent<IHoverable>(out var hoverable) && hoverable.CanBeHovered())
        {
            SetHoverable(hoverable);
        }
        else
        {
            SetHoverable(null);
        }
    }

    private void SetHoverable(IHoverable hoverable)
    {
        if (_currentHoverable == hoverable)
            return;

        _currentHoverable?.OnHoverExited();
        _currentHoverable = hoverable;
        _currentHoverable?.OnHoverEntered();
    }
}
