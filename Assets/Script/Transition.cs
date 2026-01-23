using UnityEngine;

public class Transition : MonoBehaviourSingleton<Transition>
{
    [SerializeField] private Animator _animator;

    public void PlayFadeAnimation()
    {
        _animator.SetTrigger("FadeAnimation");
    }
    
    public void OnTransitionPeakReached()
    {
        Debug.Log("Transition peak reached.");
    }
}