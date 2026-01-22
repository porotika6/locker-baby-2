using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DetectClick : MonoBehaviour
{
    public UnityEvent Clicked;

    private const float ForwardDistanceFromCamera = 20f;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        var pointer = Pointer.current;
        Vector2 currentClickPos = pointer.position.ReadValue();

        if (pointer.press.wasReleasedThisFrame)
        {
            Ray ray = _cam.ScreenPointToRay(currentClickPos);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, ForwardDistanceFromCamera);
            Collider2D other = hit.collider;
            if (other && other.gameObject == gameObject)
            {
                Clicked?.Invoke();
                Debug.Log($"Clicked <color=yellow>{gameObject.name}</color>");
            }
        }
    }
}
