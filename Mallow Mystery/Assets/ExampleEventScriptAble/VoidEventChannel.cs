using System.Collections.Generic;
using UnityEngine;

namespace ExampleEventScriptAble
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannel : ScriptableObject
    {
        private List<VoidEventListener> _listeners = new List<VoidEventListener>();
    
        public void Raise()
        {
            for (int i = _listeners.Count -1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered();
            }
        }
        public void AddListener(VoidEventListener listener)
        {
            _listeners.Add(listener);
        }
        public void RemoveListener(VoidEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
