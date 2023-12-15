using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map_Journal : MonoBehaviour
{
    [SerializeField] private GameObject RenderMap;
    [SerializeField] private GameObject ImageMap;
    [SerializeField] private GameObject Marker;
    [SerializeField] private bool FixedMap;
    [SerializeField] private Vector2 MarkerPosision;

    [SerializeField] private InputActionAsset input;
    // Start is called before the first frame update
    void Start()
    {
        if (FixedMap)
        {
            RenderMap.SetActive(false);
            ImageMap.SetActive(true);
        }
        else
        {
            RenderMap.SetActive(true);
            ImageMap.SetActive(false);
            Marker.transform.position = new Vector3(MarkerPosision.x, 0, MarkerPosision.y);
        }
    }
    
}
