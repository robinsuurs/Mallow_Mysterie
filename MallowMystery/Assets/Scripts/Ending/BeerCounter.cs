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
    [SerializeField] private float timeBeforeAudioEndStartEvent;
    
    [SerializeField] private UnityEvent endingEvent;

    private bool waitForSound = false;
    private float timer = 0;

    public void beerDrunkAdd() {
        beerDrunk++;
        
        if (beerDrunk < 5) return;
        
        input.Disable();
        waitForSound = true;
        aud.Play();
        StartCoroutine(PlayEnding());
    }

    private IEnumerator PlayEnding() {
        while (timer < aud.clip.length - timeBeforeAudioEndStartEvent) {
            timer += Time.deltaTime;
            yield return null;
        }
        endingEvent.Invoke();
    }

    public void LoadData(GameData data) {
        beerDrunk = data.beerDrunk;
    }

    public void SaveData(ref GameData data) {
        data.beerDrunk = beerDrunk;
    }
}
