using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color highlitedColor;
    [Space] [SerializeField] private Transform exitDoorPosition;

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = sprite.color;
    }

    private void OnMouseEnter()
    {
        sprite.color = highlitedColor;
    }

    private void OnMouseExit()
    {
        sprite.color = _defaultColor;
    }

    private void OnMouseDown()
    {
        
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        playerInteraction.transform.position = exitDoorPosition.position;
    }
}