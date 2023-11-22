using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    [SerializeField] private CharacterController controller;
    [SerializeField] private InputActionAsset plInputAction;
    [SerializeField] private Camera mainCamera;
    private InputAction _moveInputAction;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    private void Start()
    {
        //rotate the player relative to the camera
        mainCamera = FindObjectOfType<Camera>();
        transform.Rotate(new Vector3(0,mainCamera.transform.eulerAngles.y,0));
        
        //get the move action for value polling
        _moveInputAction = plInputAction.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        //poll value of the input value vector(x,y)
        _moveInput = _moveInputAction.ReadValue<Vector2>();
        
        //transform the input vector to the local direction of the player
        //note transform seems to be wrong way around but has the correct output
        _moveDirection = transform.TransformVector(_moveInput.x,0,_moveInput.y);

        //move the player
        var outputSpeed = _moveDirection * speed;
        controller.SimpleMove(outputSpeed);
        print(controller.velocity.magnitude);
    }

}
    
    

    
      
    


