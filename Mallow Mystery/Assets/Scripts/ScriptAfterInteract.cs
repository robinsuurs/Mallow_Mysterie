using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAfterInteract : MonoBehaviour
{
    public void moveUp()
    {
        transform.Translate(new Vector3(0,0,10));
    }
}
