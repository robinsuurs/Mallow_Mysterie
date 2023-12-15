using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



enum AnimationState
{
    Idle,
    Walk,
    Run
}
public class CharacterAnimations : MonoBehaviour
{
    private Animator animator;

    private AnimationState _state;

    [SerializeField] private GameObject Player;
    [SerializeField] private InputActionAsset inpAsset;
    private InputActionMap IAmap;
    private InputAction movementAction;
    // private InputSystem input;

    private void Awake()
    {
        movementAction = inpAsset.FindAction("Move");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        _state = AnimationState.Idle;
        StartCoroutine(IdleCycle());
    }

    private void OnEnable()
    {
        movementAction.started += setWalking;
        movementAction.canceled += setIdle;
    }

    private void OnDisable()
    {
        movementAction.started -= setWalking;
        movementAction.canceled -= setIdle;
    }

    void setIdle(InputAction.CallbackContext c)
    {
        _state = AnimationState.Idle;
        StartCoroutine(IdleCycle());
    }

    void setWalking(InputAction.CallbackContext c)
    {
        _state = AnimationState.Walk;
        animator.SetTrigger("Walk");
    }

    void setRunning(InputAction.CallbackContext c)
    {
        _state = AnimationState.Run;
        animator.SetTrigger("Run");
    }


    private void Update()
    {
        if (_state != AnimationState.Idle)
        {
            Vector2 direction = movementAction.ReadValue<Vector2>();
            
            var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, targetAngle+transform.parent.rotation.eulerAngles.y, 0);
            
        }
    }


    IEnumerator IdleCycle()
    {
        while (_state == AnimationState.Idle)
        {
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(5f);
            if ((_state != AnimationState.Idle)) break;
            animator.SetTrigger("Idle2");
            yield return new WaitForSeconds(2f);
        }
    }
}
