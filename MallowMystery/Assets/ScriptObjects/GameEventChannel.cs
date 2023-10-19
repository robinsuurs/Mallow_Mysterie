using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

namespace ExampleEventScriptAble
{
    [CreateAssetMenu(menuName = "Events/GameEventChannel")]
    public class GameEventChannel : ScriptableObject
    {
        private readonly List<GameEventListeners> _listeners = new List<GameEventListeners>();
    
        public void Raise()
        {
            for (int i = _listeners.Count -1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered();
            }
        }
        public void AddListener(GameEventListeners listener)
        {
            _listeners.Add(listener);
        }
        public void RemoveListener(GameEventListeners listener)
        {
            _listeners.Remove(listener);
        }

        public List<GameEventListeners> GetListenersList() {
            return _listeners;
        } 
    }
}
