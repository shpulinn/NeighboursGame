using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float distanceToInteract = .4f;

    private IInteractable _currentInterction;
    private Transform _currentInteractionTransform;

    private void Start()
    {
        _currentInterction = null;
        _currentInteractionTransform = null;
    }

    private void Update()
    {
        if (_currentInterction == null) return;
        
        if (Vector3.Distance(transform.position, _currentInteractionTransform.position) >= distanceToInteract)
        {
            return;
        }
        
        _currentInterction.Interact(this);
        _currentInterction = null;
    }

    private void HandleTouch(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(position));
        if(hit.collider != null)
        {
            //Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                _currentInterction = interactable;
                _currentInteractionTransform = hit.transform;
                return;
            }
        }

        _currentInterction = null;
    }
    
    private void OnEnable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouchLMB += HandleTouch;
            InputHandler.Instance.OnTouchRMB += HandleTouch;
        }
    }

    private void OnDisable()
    {
        if (InputHandler.Instance != null)
        {
            InputHandler.Instance.OnTouchLMB -= HandleTouch;
            InputHandler.Instance.OnTouchRMB -= HandleTouch;
        }
    }

    private void OnDrawGizmos()
    {
        if (_currentInteractionTransform == null)
        {
            return;
        }
        Gizmos.DrawLine(transform.position, _currentInteractionTransform.position);
    }
}
