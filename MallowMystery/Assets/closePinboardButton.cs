using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class closePinboardButton : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameObject board;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
    	board.SetActive(false);
    }
}
