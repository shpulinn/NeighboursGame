using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void HandleTouch(Vector3 position)
    {
        //Debug.Log("Touch position: " + position);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.main.nearClipPlane));
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
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
