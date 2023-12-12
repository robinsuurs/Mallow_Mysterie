using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ending")]
public class EndingStringList : ScriptableObject {
    [SerializeField] private List<string> endingScriptList;
    
    public List<string> getEndingScriptList() {
        return endingScriptList;
    }
}
