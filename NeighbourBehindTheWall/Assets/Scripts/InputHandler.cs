using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    // singletone
    public static InputHandler Instance { get; private set; }

    private Vector3 touchPosition;
    private bool isTouching;
    
    // event
    public event Action<Vector3> OnTouchLMB;
    public event Action<Vector3> OnTouchRMB;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        // desktop
        if (Application.platform == RuntimePlatform.WindowsPlayer || 
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer ||
            Application.isEditor || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Input.mousePosition;
                isTouching = true;
                OnTouchLMB?.Invoke(touchPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isTouching = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                touchPosition = Input.mousePosition;
                isTouching = true;
                OnTouchRMB?.Invoke(touchPosition);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isTouching = false;
            }
        }
        // mobile devices
        else if (Application.platform == RuntimePlatform.Android || 
                 Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPosition = touch.position;
                isTouching = touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
                if (touch.phase == TouchPhase.Began)
                {
                    OnTouchLMB?.Invoke(touchPosition);
                }
            }
            else
            {
                isTouching = false;
            }
        }
    }
    
    public Vector3 GetTouchPosition()
    {
        return touchPosition;
    }

    public bool IsTouching()
    {
        return isTouching;
    }
}
