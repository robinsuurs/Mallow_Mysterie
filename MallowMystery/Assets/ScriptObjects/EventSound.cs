using System.Collections.Generic;
using UnityEngine;

namespace ScriptObjects {
    [CreateAssetMenu(menuName = "Events/EventSound")]
    public class EventSound : ScriptableObject
    {
        private readonly List<EventSoundListener> _listeners = new List<EventSoundListener>();
    
        public void Raise(AudioClip value)
        {
            for (int i = _listeners.Count -1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(value);
            }
        }
        public void AddListener(EventSoundListener listener)
        {
            _listeners.Add(listener);
        }
        public void RemoveListener(EventSoundListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
