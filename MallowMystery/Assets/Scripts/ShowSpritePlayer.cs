using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpritePlayer : MonoBehaviour {

    [SerializeField] public Transform lookat;
    [SerializeField] public Vector3 offset;

    [SerializeField] private Camera camera;

    private void LateUpdate() {
        Vector3 pos = camera.WorldToScreenPoint(lookat.position + offset);

        if (transform.position != pos) {
            transform.position = pos;
        }
    }
}
