using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private bool _isMoving = false;

    private Vector3 _movingPosition;
    
    private void Update()
    {
        if (_isMoving == false)
            return;
         if (Mathf.Abs(transform.position.x - _movingPosition.x) >= 0.1f)
         {
             _movingPosition.y = transform.position.y;
             transform.position = Vector3.MoveTowards(transform.position, _movingPosition, moveSpeed * Time.deltaTime);
         }
        else _isMoving = false;
    }

    private void HandleTouch(Vector3 position)
    {
        //Debug.Log("Touch position: " + position);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.main.nearClipPlane)); 
        _movingPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        _isMoving = true;
    }
    
    private void OnEnable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouch += HandleTouch;
        }
    }

    private void OnDisable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouch -= HandleTouch;
        }
    }
}
