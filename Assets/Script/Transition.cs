using UnityEngine;

public class Transition : MonoBehaviour
{
    public static Transition Instance { get; private set; }

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        // Cek apakah Instance sudah ada
        if (Instance != null && Instance != this)
        {
            // Jika sudah ada instance lain, hancurkan objek ini (duplikat)
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Mengambil komponen di Awake lebih aman untuk Singleton
        _animator = GetComponent<Animator>();
    }

    public void PlayFadeAnimation()
    {
        _animator.SetTrigger("FadeAnimation");
    }
    
    public void OnTransitionPeakReached()
    {
        Debug.Log("Transition peak reached.");
    }
}