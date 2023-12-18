using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;

public class CheckItemsPickedUp : MonoBehaviour {
    [SerializeField] private List<ItemData> checkItems;
    [SerializeField] private List<ItemData> needOneOfList;
    [SerializeField] private UnityEvent nothing;
    [SerializeField] private UnityEvent enough;

    public void checkLeave() {
        if (checkItems.Any(item => item.hasBeenPickedUp == false) || needOneOfList.All(item => item.hasBeenPickedUp == false)) {
            nothing.Invoke();
            return;
        }

        enough.Invoke();
    }
}
