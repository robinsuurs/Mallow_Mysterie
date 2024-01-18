
using UnityEngine;

public class CloseToClueTrigger : MonoBehaviour
{
    private Renderer _effectRender;
    // private BoxCollider _collider;
    private void Start()
    {
      _effectRender = GetComponent<Renderer>();
      _effectRender.enabled = false;

      // _collider = GetComponentInParent<BoxCollider>();
      
    }


    private void OnTriggerEnter(Collider other)
    {
        _effectRender.enabled = true;
        foreach (var var in _effectRender.materials)
        {
            var.SetFloat("_Start_Time", Time.timeSinceLevelLoad);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        _effectRender.enabled = false;
    }
}
