using UnityEngine;
using UnityEngine.Events;

public class ClickableItem : MonoBehaviour, IClickable
{
    public bool IsClickable = true;
    public UnityEvent Clicked;

    // OnClicked is called by ClickManager when the item is clicked
    public void OnClicked()
    {
        TransitionManager.Instance.PlayTransition(() =>
        {
            Clicked?.Invoke();
            Debug.Log($"Clicked <color=yellow>{gameObject.name}</color>");
        });
    }

    // Determines whether the object can be clicked (checked by ClickManager)
    public bool CanBeClicked()
        => IsClickable && enabled && !TransitionManager.Instance.IsTransitioning;
}
