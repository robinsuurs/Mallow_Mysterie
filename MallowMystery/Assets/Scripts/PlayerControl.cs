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
    void FixedUpdate()
    {
        transform.Translate(new Vector3(_movement.x,0,_movement.y)* (speed*Time.deltaTime));
    }

    void OnMove(InputValue inputValue)
    {
        // _movement = inputValue.Get<Vector2>();
        _movement = Rotate(inputValue.Get<Vector2>(), -45);
    }

    private static Vector2 Rotate(Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    void OnResetLocation() {
        transform.position = new Vector3(0, 1, 0);
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
