using System;
using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [Header("Menu Buttons")] 
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitGame;
    //Video: https://www.youtube.com/watch?v=ijVA5Z-Mbh8

    private void Start() {
        if (!DataPersistenceManager.instance.hasGameData()) {
            continueGameButton.interactable = false;
        }
        DataPersistenceManager.instance.resetToStandardValues();
    }

    public void OnNewGameClicked() {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(DataPersistenceManager.instance.getSceneToLoadForMainMenu());
    }
    
    public void OnContinueClicked() {
        DisableMenuButtons();
        DataPersistenceManager.instance.setFromMainMenu();
        SceneManager.LoadSceneAsync(DataPersistenceManager.instance.getSceneToLoadForMainMenu());
    }
    
    public void OnExitClicked() {
        DisableMenuButtons();
        Application.Quit();
    }

    private void DisableMenuButtons() {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
        settingsButton.interactable = false;
        exitGame.interactable = false;
    }
}
