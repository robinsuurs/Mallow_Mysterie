using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour, IDataPersistence
{
    
    private Rigidbody _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float speed = 10;
    

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


    public void LoadData(GameData data) {
        this.transform.position = data.playerLocation;
    }

    public void SaveData(ref GameData data) {
        data.playerLocation = this.transform.position;
    }
}