using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIClue : MonoBehaviour, IPointerClickHandler
{
    public bool collected;
//    [SerializeField] private PinboardManager manager;
    [SerializeField] private UnityEvent pickupEvent;

    private void Start()
    {
        collected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        collected = true;
        gameObject.SetActive(false);
        pickupEvent?.Invoke();
    }
}
