using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{

    private Vector2 _movement;
    [SerializeField] private float speed = 10;
    private Object _interactable;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(_movement.x,0,_movement.y)* (speed*Time.deltaTime));
    }

    void OnMove(InputValue inputValue)
    {
        _movement = inputValue.Get<Vector2>();
    }

    private void OnCollisionStay(Collision other)
    {
        _interactable = other.gameObject.TryGetComponent(out IInteractable interactableObject) ? other.collider.GetComponent<Object>() : null;
    }

    void OnInteract()
    {
        if(_interactable != null)
        {
            _interactable.Interactable();
            Debug.Log("INTERACT PRESS");
        }
    }
}
