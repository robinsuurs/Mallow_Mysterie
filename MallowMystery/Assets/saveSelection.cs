using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class saveSelection : MonoBehaviour
{
    public void saveSelectionToGameData(int value) {
        string answer = transform.GetComponent<TMP_Dropdown>().options[value].text;
        DataPersistenceManager.instance.getGameData().setDropdownInfo(gameObject.name, answer);
    }
}
