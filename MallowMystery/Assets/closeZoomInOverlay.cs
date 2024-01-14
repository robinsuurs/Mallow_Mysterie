using System;
using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.UI;

public class closeZoomInOverlay : MonoBehaviour {
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image clueImage;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse0)) {
            canvas.SetActive(false);
        }
    }

    public void SetImage(ItemData itemData) {
        clueImage.sprite = itemData.icon;
    }
}
