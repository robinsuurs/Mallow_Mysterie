using System.Collections;
using System.Collections.Generic;
using Dialogue.RunTime;
using UnityEngine;
using UnityEngine.Events;

public class BeerCounter : MonoBehaviour, IDataPersistence {
    [SerializeField] private int beerDrunk = 0;

    [SerializeField] private UnityEvent _event;
    [SerializeField] private UnityEvent endingEvent;

    public void askBartender() {
        _event.Invoke();
    }

    public void beerDrunkAdd() {
        beerDrunk++;
        if (beerDrunk >= 5) {
            endingEvent.Invoke();
        }
    }
    
    public void LoadData(GameData data) {
        beerDrunk = data.beerDrunk;
    }

    public void SaveData(ref GameData data) {
        data.beerDrunk = beerDrunk;
    }
}
