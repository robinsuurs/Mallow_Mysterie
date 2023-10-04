using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpriteListener : MonoBehaviour
{
    public SwitchSpriteEvent Event;
    [SerializeField] private ListOfSprites _listOfSprites;
    private void OnEnable() {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(string speakerLeft, string speakerRight) {
        _listOfSprites.CharacterSetter(speakerLeft, speakerRight);
    }
}
