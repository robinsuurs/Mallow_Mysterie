
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropdownInfo {
    public string nameDropdown;
    public string value;

    public DropdownInfo(string nameDropdown, string value) {
        this.nameDropdown = nameDropdown;
        this.value = value;
    }
}
