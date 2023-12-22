using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CluePageUpdate : MonoBehaviour
{
    [SerializeField] private InventoryScreen manager;


    void OnEnable()
    {
        manager.setInventoryItems(0);
    }
}
