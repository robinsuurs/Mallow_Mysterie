using System;
using ExampleEventScriptAble;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptObjects
{
    
    public class GameEventListeners : MonoBehaviour
    {
        public GameEventChannel Event;
        public UnityEvent response;
        

        private void OnTriggerEnter(Collider other)
        {
            Event.AddListener(this);
        }

        private void OnTriggerExit(Collider other)
        {
            Event.RemoveListener(this);
        }

        private void OnDisable()
        {
            Event.RemoveListener(this);
        }

        public void OnEventTriggered()
        {
            response.Invoke();
        }
    }
}
