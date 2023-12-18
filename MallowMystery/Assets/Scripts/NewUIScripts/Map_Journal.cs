using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map_Journal : MonoBehaviour
{
    [SerializeField] [CanBeNull] private GameObject RenderMap;
    [SerializeField] private GameObject ImageMap;
    [SerializeField] private RectTransform Marker;
    [SerializeField] private bool FixedMap;
    [SerializeField] private Vector2 MarkerPosision;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (FixedMap)
        {
            ImageMap.SetActive(true);
            Marker.anchoredPosition = new Vector2(MarkerPosision.x, MarkerPosision.y);
            if (RenderMap)
            {
                RenderMap.SetActive(false);
            }
        }
        else
        {
            ImageMap.SetActive(false);
            if (RenderMap)
            {
                RenderMap.SetActive(true);
            }
        }
    }
    
}
