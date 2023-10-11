using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using ScriptObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour, IDataPersistence
{
    
    private Rigidbody _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float speed = 10;
    [SerializeField] private Inventory _inventory;

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

    void OnCheckInv() {
        foreach (var item in _inventory.items) {
            Debug.Log(item.itemName + " " + item.hasBeenPickedUp);
        }
    }
    
    public void LoadData(GameData data) {
        this.transform.position = data.playerLocation;
    }

    public void SaveData(ref GameData data) {
        data.playerLocation = this.transform.position;
    }
}
