using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PermissionCheckSave
{
    public string UID;
    public bool permission;

    public PermissionCheckSave(string UID, bool permission) {
        this.UID = UID;
        this.permission = permission;
    }
}
