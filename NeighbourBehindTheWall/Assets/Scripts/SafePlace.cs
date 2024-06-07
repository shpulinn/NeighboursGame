using System;
using UnityEngine;

public class SafePlace : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color highlitedColor;

    private Color _defaultColor;
    
    public void Interact(PlayerInteraction playerInteraction)
    {
        playerInteraction.GetComponentInChildren<PlayerVisual>().Hide();
    }
    
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
}
