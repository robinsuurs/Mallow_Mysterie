using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.InputSystem;


public class PinboardManager : MonoBehaviour
{
    [SerializeField] private GameObject pinboard;
    [SerializeField] private GameEventStandardAdd closePinBoard;
    [SerializeField] private GameEventStandardAdd openUIElement;
    [SerializeField] private GameEventStandardAdd closeUIElement;

    public void openBoard(){
    	pinboard.SetActive(true);
        openUIElement.Raise();
    }
    public void closeBoard(){
    	pinboard.SetActive(false);
        closePinBoard.Raise();
        closeUIElement.Raise();
    }
}
