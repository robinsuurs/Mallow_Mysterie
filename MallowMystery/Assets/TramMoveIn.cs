using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TramMoveIn : MonoBehaviour, IDataPersistence {
    [SerializeField] private bool tramMoved = false;
    [SerializeField] private GameObject tram;
    [SerializeField] private Vector3 tramLoc;
    [SerializeField] private Vector3 playerSpawnLoc;
    [SerializeField] private float tramSpeed;
    [SerializeField] private GameObject player;
    [SerializeField] private InputActionAsset _inputAction;
    
    public void Start() {
        if (!tramMoved) {
            StartCoroutine(TramRide());
        } else {
            tram.transform.position = tramLoc;
        }
    }

    IEnumerator TramRide() {
        _inputAction.Disable();
        Vector3 oldTramLoc = tram.transform.position;
        while (tram.transform.position != tramLoc) {
            tram.transform.position = Vector3.MoveTowards(tram.transform.position, tramLoc, Time.deltaTime * tramSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        
        Instantiate(player, playerSpawnLoc, quaternion.identity);
        
        yield return new WaitForSeconds(1.5f);
        
        while (tram.transform.position != oldTramLoc) {
            tram.transform.position = Vector3.MoveTowards(tram.transform.position, oldTramLoc, Time.deltaTime * tramSpeed);
            yield return null;
        }
        
        _inputAction.Enable();
        Camera.main.GetComponent<Follow_Player>().enabled = true;
        Camera.main.GetComponent<Follow_Player>().setFollowPlayer();
    }

    public void LoadData(GameData data) {
        tramMoved = data.tramMovedInPoor;
    }

    public void SaveData(ref GameData data) {
        data.tramMovedInPoor = tramMoved;
    }
}
