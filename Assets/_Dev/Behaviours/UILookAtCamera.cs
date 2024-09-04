using UnityEngine;

public class UILookAtCamera: MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update() 
    { 
        transform.LookAt(mainCamera.transform);
    }
}