using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void OnNewGameClicked () {
        DataPersistenceManager.instance.NewGame();
    }

    public void OnLoadGameClicked() {
        DataPersistenceManager.instance.LoadGame();
    }

    public void OnSaveGameClicked() {
        DataPersistenceManager.instance.SaveGame();
    }
}
