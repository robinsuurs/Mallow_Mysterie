using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map_Journal : MonoBehaviour
{
    [SerializeField] [CanBeNull] private GameObject RenderMap;
    [SerializeField] private GameObject[] ImageMaps;
    [SerializeField] private RectTransform Marker;
    [SerializeField] private bool FixedMap;
    [SerializeField] private int ImageMapIndex;
    [SerializeField] private Vector2 MarkerPosision;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (GameObject m in ImageMaps)
        {
            m.SetActive(false);
        }
        if (FixedMap)
        {
            GameObject map = ImageMaps[ImageMapIndex];
            map.SetActive(true);
            Marker.anchoredPosition = new Vector2(MarkerPosision.x, MarkerPosision.y);
            if (RenderMap)
            {
                RenderMap.SetActive(false);
            }
        }
        else
        {
            Marker.localScale = new Vector3(0, 0, 0);
            if (RenderMap)
            {
                RenderMap.SetActive(true);
            }
        }
    }
    
}
