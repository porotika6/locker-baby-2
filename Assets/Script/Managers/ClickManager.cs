using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public interface IClickable
{
    /// <summary>
    /// Determines whether the object can be clicked.
    /// </summary>
    public bool CanBeClicked() => true;

    /// <summary>
    /// Called when the object is clicked.
    /// </summary>
    public void OnClicked();
}

/// <summary>
/// Manages click interactions in the game world.
/// Detects clicks on game objects that implement the IClickable interface.
/// Handles click detection with drag distance threshold to distinguish between clicks and drags.
/// </summary>
/// <remarks>
/// This manager is automatically initialized at runtime as a singleton and persists across scene loads.
/// It uses the new Input System (UnityEngine.InputSystem) to detect pointer input.
/// Raycasting is performed in 2D space against objects on specified layers.
/// </remarks>
public class ClickManager : MonoBehaviour
{
    public LayerMask ClickableLayers = -1; // -1 means all layers

    private const float ClickDistanceThreshold = 5f;
    private const float ForwardDistanceFromCamera = 20f;

    private Camera _cam;
    private Vector2 _clickStartPos;

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        var clickManagerObj = new GameObject("ClickManager");
        clickManagerObj.AddComponent<ClickManager>();
        DontDestroyOnLoad(clickManagerObj);
    }

    private void Awake()
    {
        Assert.IsNotNull(Camera.main, "Main Camera not found in the scene.");
        _cam = Camera.main;

        // Detect if there are multiple instances
        if (FindObjectsByType<ClickManager>(FindObjectsSortMode.None).Length > 1)
            Debug.LogError("Multiple ClickManager instances detected. There should only be one ClickManager in the scene.");
    }

    private void Update()
    {
        var pointer = Pointer.current;
        if (pointer.press.wasPressedThisFrame)
        {
            _clickStartPos = pointer.position.ReadValue();
        }
        else if (pointer.press.wasReleasedThisFrame)
        {
            Vector2 currentClickPos = pointer.position.ReadValue();
            
            // Check the distance to ensure it's a click and not a drag
            if ((_clickStartPos - currentClickPos).sqrMagnitude < ClickDistanceThreshold * ClickDistanceThreshold)
            {
                Ray ray = _cam.ScreenPointToRay(currentClickPos);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, ForwardDistanceFromCamera, ClickableLayers);
                Collider2D other = hit.collider;
                if (other && other.TryGetComponent(out IClickable clickable) && clickable.CanBeClicked())
                {
                    clickable.OnClicked();
                }
            }
        }
    }
}
