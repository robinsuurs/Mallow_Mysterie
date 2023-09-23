using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public VoidEventChannel voidEventChannel;
    private Rigidbody _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float speed = 10;

    // private EventManager _eventManager;

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
    public void OnTriggerEnter(Collider other)
    {
        // _eventManager.hit.Invoke();
    }

    void OnInteract(InputValue inputValue)
    {
        Debug.Log("test");
        voidEventChannel.Raise();
    }
}
