using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSoundInitializer : MonoBehaviour
{
    [SerializeField] private bool inside;
    // Start is called before the first frame update
    public void setWalkingSound()
    {
        GameObject Milton = GameObject.Find("Milton4Animations");
        if (Milton) print("foundHim");
        var comp = Milton.GetComponent<WalkingSound>();
        comp.inside = inside;
    }

}
