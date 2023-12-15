using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Dropdown _dropdown;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        _dropdown.Show();
    }
}
