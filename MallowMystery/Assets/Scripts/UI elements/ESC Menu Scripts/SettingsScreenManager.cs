using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScreenManager : MonoBehaviour
{
    private GameObject currentShownGameObject;
    
    public void showSettingsScreen(string gameObjectShown) {
        if (!SceneManager.GetActiveScene().name.Equals("OverworldMap")) {
            this.gameObject.transform.Find("MenuContainer").transform.Find("MapButton").GetComponent<Button>().interactable = false;
        }
        if (this.gameObject.activeSelf && (gameObjectShown.Equals("OpenSettings") || currentShownGameObject.name.Equals(gameObjectShown))) {
            currentShownGameObject.SetActive(false);
            currentShownGameObject = null;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        } else if (gameObjectShown.Equals("OverworldMap") && !SceneManager.GetActiveScene().name.Equals(gameObjectShown)) {
            
        } else {
            if (!this.gameObject.activeSelf) {
                this.gameObject.SetActive(true);
            }
            Time.timeScale = 0f;
            if (currentShownGameObject != null) {
                currentShownGameObject.SetActive(false);
            }
            if (gameObjectShown.Equals("OpenSettings")) {
                gameObjectShown = "MainSettings";
            }
            this.currentShownGameObject = this.gameObject.transform.Find("MenuContainer").transform.Find(gameObjectShown).gameObject;
            this.currentShownGameObject.SetActive(true);
            if (gameObjectShown.Equals("Inventory")) {
                currentShownGameObject.transform.GetComponent<InventoryScreen>().setInventoryItems(0);
            }
        }
    }
}
