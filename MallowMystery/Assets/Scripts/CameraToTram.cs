using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class CameraToTram : MonoBehaviour, IDataPersistence {
    [SerializeField] private InputActionAsset _inputAction;
    
    [SerializeField] private bool cameraHasPanned = false;
    [SerializeField] private GameObject tram;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    
    private int cameraPanningState = 0;

    private Vector3 orignalLocation;
    private Vector3 targetLocation;
    
    private Camera mainCam;

    public void moveCamera(bool value) {
        if (cameraHasPanned || !value) return;
        
        _inputAction.Disable();
        mainCam = Camera.main;

        var position = mainCam.transform.position;
        orignalLocation = position;

        Vector3 offSet = mainCam.GetComponent<Follow_Player>().getCameraOffset();
        var tramPosition = tram.transform.position;
        targetLocation = new Vector3(tramPosition.x + offSet.x, position.y,tramPosition.z + offSet.z);
        Debug.Log(targetLocation);
        
        
        mainCam.GetComponent<Follow_Player>().enabled = false;
        
        cameraPanningState = 1;
    }

    private void LateUpdate() {
        if (cameraPanningState == 0) return;

        switch (cameraPanningState) {
            case 1 :
                if(mainCam.transform.position.x != targetLocation.x && mainCam.transform.position.z != targetLocation.z) {
                    mainCam.transform.position =
                        Vector3.MoveTowards(mainCam.transform.position, targetLocation, Time.deltaTime * speed);
                } else {
                    cameraPanningState = 0;
                    StartCoroutine(focusOnTram());
                }
                break;
            case 2 :
                if(mainCam.transform.position.x != orignalLocation.x && mainCam.transform.position.z != orignalLocation.z) {
                    
                    mainCam.transform.position =
                        Vector3.MoveTowards(mainCam.transform.position, orignalLocation, Time.deltaTime * speed);
                } else {
                    mainCam.GetComponent<Follow_Player>().enabled = true;
                    cameraHasPanned = true;
                    cameraPanningState = 0;
                    _inputAction.Enable();
                }
                break;
        }
        
    }

    IEnumerator focusOnTram() {
        yield return new WaitForSeconds(waitTime);
        cameraPanningState = 2;
    }


    public void LoadData(GameData data) {
        cameraHasPanned = data.cameraHasPanned;
    }

    public void SaveData(ref GameData data) {
        data.cameraHasPanned = cameraHasPanned;
    }
}
