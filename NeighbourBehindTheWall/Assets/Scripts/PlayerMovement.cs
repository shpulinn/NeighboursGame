using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeedNormal = 5;
    [SerializeField] private float moveSpeedSlow = 2f;

    private float _currentMoveSpeed;
    
    private bool _isMoving = false;

    private Vector3 _movingPosition;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // включить UI кнопку для переключения номарльного и медленного шага
        }

        _currentMoveSpeed = moveSpeedNormal;
    }

    private void Update()
    {
        if (_isMoving == false)
            return;
        if (Mathf.Abs(transform.position.x - _movingPosition.x) >= 0.1f)
        {
         _movingPosition.y = transform.position.y;
         transform.position = Vector3.MoveTowards(transform.position, _movingPosition, _currentMoveSpeed * Time.deltaTime);
        }
        else _isMoving = false;
    }

    private void HandleTouchLmb(Vector3 position)
    {
        _currentMoveSpeed = moveSpeedNormal;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.main.nearClipPlane)); 
        _movingPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        _isMoving = true;
    }
    
    private void HandleTouchRmb(Vector3 position)
    {
        _currentMoveSpeed = moveSpeedSlow;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.main.nearClipPlane)); 
        _movingPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        _isMoving = true;
    }
    
    private void OnEnable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouchLMB += HandleTouchLmb;
            InputHandler.Instance.OnTouchRMB += HandleTouchRmb;
        }
    }

    private void OnDisable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouchLMB -= HandleTouchLmb;
            InputHandler.Instance.OnTouchRMB -= HandleTouchRmb;
        }
    }
}
