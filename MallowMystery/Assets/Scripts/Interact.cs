using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using UnityEngine;

public class Interact : MonoBehaviour
{
   public GameEventChannel interact;
   void OnInteract()
   {
       interact.Raise();
   }
}
