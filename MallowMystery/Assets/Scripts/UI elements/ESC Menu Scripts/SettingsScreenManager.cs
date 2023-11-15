using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScreenManager : MonoBehaviour
{
    private GameObject currentShownGameObject;
    [SerializeField] private InputActionAsset _inputAction;
    
    public void showSettingsScreen(string gameObjectShown) {
        //Block opening off map in other scenes
        if (!SceneManager.GetActiveScene().name.Equals("OverworldMap")) {
            gameObject.transform.Find("MenuContainer").transform.Find("MapButton").GetComponent<Button>().interactable = false;
        }
        
        if (gameObject.activeSelf && gameObjectShown.Equals("OpenSettings")) {
            currentShownGameObject.SetActive(false);
            currentShownGameObject = null;
            gameObject.SetActive(false);
            enableInputKeys();
            Time.timeScale = 1;
        } else {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
                disableInputKeys();
                Time.timeScale = 0f;
            }
            
            if (currentShownGameObject != null) {
                currentShownGameObject.SetActive(false);
            }
            
            if (gameObjectShown.Equals("OpenSettings")) {
                gameObjectShown = "MainSettings";
            }
            currentShownGameObject = gameObject.transform.Find("MenuContainer").transform.Find(gameObjectShown).gameObject;
            currentShownGameObject.SetActive(true);
            if (gameObjectShown.Equals("Inventory")) {
                currentShownGameObject.transform.GetComponent<InventoryScreen>().setInventoryItems(0);
            }
        }
    }

    private void disableInputKeys() {
        _inputAction["Move"].Disable();
        _inputAction["Interact"].Disable();
        _inputAction["OpenInventory"].Disable();
        _inputAction["OpenMap"].Disable();
    }

    private void enableInputKeys() {
        _inputAction["Move"].Enable();
        _inputAction["Interact"].Enable();
        _inputAction["OpenInventory"].Enable();
        _inputAction["OpenMap"].Enable();
    }
}
