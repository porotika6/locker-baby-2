using System;
using UnityEngine;

public class TransitionManager : MonoBehaviourSingleton<TransitionManager>
{
    [SerializeField] private Animator _animator;

    public bool IsTransitioning => _isTransitioning;

    private bool _isTransitioning;
    private Action _onCutPointReached;

    public void PlayTransition(Action onCutPointReached)
    {
        _onCutPointReached = onCutPointReached;
        _animator.SetTrigger("FadeAnimation");
        _isTransitioning = true;
    }

    public void PlayTransition()
    {
        PlayTransition(null);
    }
    
    public void OnTransitionCutPointReached()
    {
        _onCutPointReached?.Invoke();
        _onCutPointReached = null;
    }

    public void OnTransitionFinished()
    {
        _isTransitioning = false;
    }
}