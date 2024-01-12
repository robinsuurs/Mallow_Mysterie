using UnityEngine;
using UnityEngine.Events;

namespace ScriptObjects {
    public class EventSoundListener : MonoBehaviour
    {
        public EventSound Event;
        public UnityEvent<AudioClip> response;
        

        private void OnEnable()
        {
            Event.AddListener(this);
        }

        private void OnDisable()
        {
            Event.RemoveListener(this);
        }

        public void OnEventTriggered(AudioClip value)
        {
            response.Invoke(value);
        }
    }
}
