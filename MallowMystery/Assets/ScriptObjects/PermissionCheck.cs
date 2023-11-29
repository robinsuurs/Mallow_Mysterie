using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Permission")]
public class PermissionCheck : ScriptableObject, IDataPersistence {
    private string UID;
    [SerializeField] private bool permission;
    [SerializeField] private UnityEvent permitted;
    [SerializeField] private UnityEvent notPermitted;

    public void setPermission(bool permission) {
        this.permission = permission;
    }

    public void raiseEvent() {
        if (permission) {
            permitted.Invoke();
        } else {
            notPermitted.Invoke();
        }
    }
    
    private void OnValidate() {
#if UNITY_EDITOR
        if (UID != "") return;
        UID = GUID.Generate().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void LoadData(GameData data) {
        foreach (var dataSave in data.PermissionCheckSaves.Where(dataSave => UID.Equals(dataSave.UID))) {
            permission = dataSave.permission;
            break;
        }
    }

    public void SaveData(ref GameData data) {
        foreach (var t in data.PermissionCheckSaves.Where(t => t.UID.Equals(UID))) {
            t.permission = permission;
            return;
        }
        data.PermissionCheckSaves.Add(new PermissionCheckSave(UID, permission));
    }
}
