using UnityEngine;

public class Transition : MonoBehaviour
{
    // 1. Variabel statis untuk menyimpan Instance
    public static Transition Instance { get; private set; }

    private Animator fadeAnim;

    private void Awake()
    {
        // 2. Cek apakah Instance sudah ada
        if (Instance != null && Instance != this)
        {
            // Jika sudah ada instance lain, hancurkan objek ini (duplikat)
            Destroy(gameObject);
            return;
        }

        // 3. Set Instance ke skrip ini
        Instance = this;

        // 4. (Opsional tapi Direkomendasikan) Agar tidak hancur saat ganti Scene
        DontDestroyOnLoad(gameObject);
        
        // Mengambil komponen di Awake lebih aman untuk Singleton
        fadeAnim = GetComponent<Animator>();
    }

    // Contoh fungsi untuk memicu animasi (bisa dipanggil dari skrip lain)
    public void PlayFadeAnimation()
    {
        if (fadeAnim != null)
        {
            fadeAnim.SetTrigger("FadeAnimation"); // Pastikan parameter "FadeIn" ada di Animator Controller
        }
    }
    
    
}