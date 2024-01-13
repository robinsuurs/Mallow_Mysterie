using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue.RunTime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BeerCounter : MonoBehaviour, IDataPersistence {
    [SerializeField] private int beerDrunk = 0;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private AudioSource aud;

    [SerializeField] private UnityEvent _event;
    [SerializeField] private UnityEvent endingEvent;

    private bool waitForSound = false;
    private float timer = 0;

    public void askBartender() {
        _event.Invoke();
    }

    public void beerDrunkAdd() {
        beerDrunk++;
        
        if (beerDrunk < 5) return;
        
        input.Disable();
        waitForSound = true;
        aud.Play();
        
    }

    private void Update() {
        if (!waitForSound) return;
        
        timer += Time.deltaTime;
        if (timer > aud.clip.length) {
            endingEvent.Invoke();
        }
    }

    public void LoadData(GameData data) {
        beerDrunk = data.beerDrunk;
    }

    public void SaveData(ref GameData data) {
        data.beerDrunk = beerDrunk;
    }
}
