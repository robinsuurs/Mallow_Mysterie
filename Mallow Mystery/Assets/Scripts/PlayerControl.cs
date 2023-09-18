using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float speed = 10;
    private bool _canInteract = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

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
        _canInteract = other.gameObject.TryGetComponent(out IInteractable interactableObject) ? true : false;

    }

    void OnInteract()
    {
        if (_canInteract)
        {
            
            Debug.Log("INETERACT PRESS");
        }
    }
}
