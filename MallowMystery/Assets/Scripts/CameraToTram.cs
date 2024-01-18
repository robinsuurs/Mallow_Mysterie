using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class CameraToTram : MonoBehaviour, IDataPersistence {
    [SerializeField] private InputActionAsset _inputAction;
    
    [SerializeField] private bool cameraHasPanned = false;
    [SerializeField] private GameObject tram;
    [SerializeField] private float camSpeed;
    [SerializeField] private float tramSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Vector3 camTramView;
    [SerializeField] private Vector3 tramEndLoc;
    [SerializeField] private Vector3 tramLoc;
    [SerializeField] private UnityEvent uEvent;

    private Vector3 orignalLocation;
    private Vector3 targetLocation;
    
    private Camera mainCam;

    public void moveCamera(bool value) {
        if (cameraHasPanned || !value) return;
        
        mainCam = Camera.main;
        orignalLocation = mainCam.transform.position;

        Vector3 offSet = mainCam.GetComponent<Follow_Player>().getCameraOffset();
        var tramPosition = tram.transform.position;
        targetLocation = new Vector3(tramPosition.x + offSet.x, orignalLocation.y,tramPosition.z + offSet.z);
        
        StartCoroutine(focusOnTram());
    }

    public void tramAway() {
        StartCoroutine(tramTramAWAY());
    }

    IEnumerator focusOnTram() {
        _inputAction.Disable();
        mainCam.GetComponent<Follow_Player>().enabled = false;
        Vector3 playerLocation = GameObject.FindWithTag("Player").transform.position;
        Vector3 offset = mainCam.GetComponent<Follow_Player>().getCameraOffset();
        orignalLocation.x = playerLocation.x + offset.x;
        orignalLocation.y = playerLocation.y + offset.y;
        orignalLocation.z = playerLocation.z + offset.z;
        Debug.Log(orignalLocation);
        while (mainCam.transform.position != camTramView) {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, camTramView, Time.deltaTime * camSpeed);
            yield return null;
        }

        while (tram.transform.position != tramLoc) {
            tram.transform.position = Vector3.MoveTowards(tram.transform.position, tramLoc, Time.deltaTime * tramSpeed);
            yield return null;
        }
        
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(waitTime);

        while (mainCam.transform.position.x != orignalLocation.x && mainCam.transform.position.z != orignalLocation.z) {
            mainCam.transform.position =
                Vector3.MoveTowards(mainCam.transform.position, orignalLocation, Time.deltaTime * camSpeed);
            yield return null;
        }
        
        mainCam.GetComponent<Follow_Player>().enabled = true;
        cameraHasPanned = true;
        _inputAction.Enable();
    }

    IEnumerator tramTramAWAY() {
        GameObject.FindWithTag("Player").gameObject.SetActive(false);
        _inputAction.Disable();
        yield return new WaitForSeconds(1);
        while (tram.transform.position != tramEndLoc) {
            tram.transform.position =
                Vector3.MoveTowards(tram.transform.position, tramEndLoc,Time.deltaTime * tramSpeed);
            yield return null;
        }
        
        uEvent.Invoke();
    }


    public void LoadData(GameData data) {
        cameraHasPanned = data.cameraHasPanned;
        if (cameraHasPanned) {
            tram.transform.position = tramLoc;
        }
    }

    public void SaveData(ref GameData data) {
        data.cameraHasPanned = cameraHasPanned;
    }
}
