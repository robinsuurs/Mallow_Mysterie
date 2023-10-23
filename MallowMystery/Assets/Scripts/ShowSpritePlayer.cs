using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using ScriptObjects;
using UnityEngine;

public class ShowSpritePlayer : MonoBehaviour {

    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private Camera cameraSprite;
    [SerializeField] private GameEventChannel interactListener;
    private bool showSprite = false;

    private void Start() {
        playerCanvas.transform.rotation = Quaternion.LookRotation(playerCanvas.transform.position - cameraSprite.transform.position);
    }

    private void showInteractableSprite() {
        showSprite = !showSprite;
        playerCanvas.SetActive(showSprite);
    }

    public void Update() {
        if ((interactListener.GetListenersList().Count != 0 && !showSprite) || (interactListener.GetListenersList().Count == 0 && showSprite)) {
            showInteractableSprite();
        }
    }
}
