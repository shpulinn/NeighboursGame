using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer visual;

    private bool _isHided = false;

    public bool IsHided => _isHided;

    public void Hide()
    {
        visual.enabled = false;
        _isHided = true;
    }

    public void Show()
    {
        visual.enabled = true;
        _isHided = false;
    }

    private void HandleTouchLmb(Vector3 vector)
    {
        if (_isHided)
            Show();
    }
    
    private void OnEnable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouchLMB += HandleTouchLmb;
        }
    }

    private void OnDisable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouchLMB -= HandleTouchLmb;
        }
    }
}
