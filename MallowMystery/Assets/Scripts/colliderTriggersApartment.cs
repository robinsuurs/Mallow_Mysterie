using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTriggersApartment : MonoBehaviour {
    [SerializeField] private int LayerMaskName;
    private void OnTriggerEnter(Collider other)
    {
        Camera.main.cullingMask |= (1 << LayerMaskName);
    }

    private void OnTriggerExit(Collider other)
    {
        Camera.main.cullingMask &= ~(1 << LayerMaskName);
    }
}
