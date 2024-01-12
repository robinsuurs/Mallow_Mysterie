using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour {
    public void backToMainMenu() {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void saveGame() {
        DataPersistenceManager.instance.SaveGame();
        }

    public void loadGame() {
        DataPersistenceManager.instance.setFromMainMenu();
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync(DataPersistenceManager.instance.getSceneToLoadForMainMenu());
    }

    public void exitgame() {
        Application.Quit();
    }
}
