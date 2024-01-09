using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Image blackBackground;
    [SerializeField] private UnityEvent unityEvent;
    [SerializeField] private InputActionAsset inputAction;

    public void FadeToBlackSequence(float fadeSpeed, int opacity, bool goToEndScreen) {
        endCanvas.SetActive(true);
        StartCoroutine(FadeToBlackTime(fadeSpeed, opacity, b => {
            if (!b || !goToEndScreen) return;
            inputAction.Enable();
            unityEvent.Invoke();
        }));
    }

    public void FadeToNormal(float fadeSpeed, int opacity, bool goToEndScreen) {
        StartCoroutine(FadeToNormalTime(fadeSpeed, b => {
            if (b) {
                endCanvas.SetActive(!b);
            }
        }));
    }

    private IEnumerator FadeToBlackTime(float fadeSpeed, int opactity, Action<bool> callback) {
        Color objectColor = blackBackground.color;
        float fadeAmount;
        float newOpacity = 1f / 255f * opactity;
        
        while (blackBackground.color.a < newOpacity) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
    
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackBackground.color = objectColor;
            yield return null;
        }
        
        callback.Invoke(true);
    }

    private IEnumerator FadeToNormalTime(float speed, Action<bool> cal) {
        Color objectColor = blackBackground.color;
        float fadeAmount;
        
        while (blackBackground.color.a > 0) {
            fadeAmount = objectColor.a - (speed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackBackground.color = objectColor;
            yield return null;
        }
        
        cal.Invoke(true);
    }
}
