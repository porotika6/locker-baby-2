using UnityEngine;
using UnityEngine.InputSystem;

public class DetectClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectClick();
    }

    void DetectClick()
    {
    
     {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        Debug.Log("Dah di klik nih");
     }
    }
}
