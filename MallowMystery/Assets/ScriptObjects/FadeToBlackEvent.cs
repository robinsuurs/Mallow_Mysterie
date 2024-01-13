using System.Collections.Generic;
using UnityEngine;

namespace ScriptObjects {
    [CreateAssetMenu(menuName = "Events/FadeToBlackEvent")]
    public class FadeToBlackEvent : ScriptableObject
    {
        private readonly List<FadeToBlackListener> _listeners = new List<FadeToBlackListener>();

        [SerializeField] private float fadeSpeed;
        [Range(0, 1)]
        [SerializeField] private float opacity;

        [SerializeField] private bool goToEndScreen;

    
        public void Raise()
        {
            for (int i = _listeners.Count -1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(fadeSpeed, opacity, goToEndScreen);
            }
        }
        public void AddListener(FadeToBlackListener listener)
        {
            _listeners.Add(listener);
        }
        public void RemoveListener(FadeToBlackListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
