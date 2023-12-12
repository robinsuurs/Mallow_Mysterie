using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ending")]
public class EndingStringList : ScriptableObject {
    [SerializeField] private List<string> endingScriptList;
    [SerializeField] private string endingNumber;
    
    public List<string> getEndingScriptList() {
        return endingScriptList;
    }

    public string getEndingNumber() {
        return endingNumber;
    }
}
