using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TestScript : MonoBehaviour, IDataPersistence
{
    public int test = 9;
    
    public void LoadData(GameData data) {
        this.test = data.test;
    }

    public void SaveData(ref GameData data) {
        data.test = this.test;
    }
}
