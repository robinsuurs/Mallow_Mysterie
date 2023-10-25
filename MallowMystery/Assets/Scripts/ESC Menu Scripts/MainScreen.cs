using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour {
    public void backToMainMenu() {
        Time.timeScale = 1;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
