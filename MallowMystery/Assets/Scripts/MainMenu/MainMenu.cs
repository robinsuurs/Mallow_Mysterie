using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [Header("Menu Buttons")] 
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    //Video: https://www.youtube.com/watch?v=ijVA5Z-Mbh8

    private void Start() {
        if (!DataPersistenceManager.instance.hasGameData()) {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked() {
        Debug.Log("Opening scene");
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("EventManagerTest");
    }
    
    public void OnContinueClicked() {
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("EventManagerTest");
    }

    private void DisableMenuButtons() {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
