using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private GameObject imageObject;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private GameObject endCanvas;
    
    [SerializeField] private UnityEvent unityEvent;

    public void fadeToBlack(EndingStringList endingStringList) {
        input.Disable();
        endCanvas.SetActive(true);
        DataPersistenceManager.instance.setEndingStringList(endingStringList);
        StartCoroutine(FadeToBlackTime());
    }
    
    private IEnumerator FadeToBlackTime() {
        Color objectColor = imageObject.GetComponent<Image>().color;
        float fadeAmount;
        
        while (imageObject.GetComponent<Image>().color.a < 1) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            imageObject.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        
        unityEvent.Invoke();
    }
}
