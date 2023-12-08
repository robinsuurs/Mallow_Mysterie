using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class newCanvasManager : MonoBehaviour//, IPointerClickHandler
{
    [SerializeField] private GameObject Journal;

    [SerializeField] private List<GameObject> pages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void OnPointerClick(PointerEventData eventData) //TODO: Doet het niet.
    // {
    //     closeJournal();
    // }

    public void closeJournal()
    {
        if (Journal.activeSelf)
        {
            Journal.SetActive(false);
            foreach (var page in pages)
            {
                page.SetActive(false);
            }
        }
    }
}
