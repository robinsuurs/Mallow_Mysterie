using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private GameObject canvasToEnableDisable; 
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UnityEvent booleanRun;

    private bool changing = false;

    public void FadeToBlackSequence(float fadeSpeed, float opacity, bool runEvent) {
        canvasToEnableDisable.SetActive(true);
        StartCoroutine(FadeToBlackTime(fadeSpeed, opacity, b => {
            if (!runEvent || changing) return;
            changing = true;
            booleanRun.Invoke();
        }));
    }

    public void FadeToNormal(float fadeSpeed, float opacity, bool runEvent) {
        StartCoroutine(FadeToNormalTime(fadeSpeed, b => {
            if (b) {
                canvasToEnableDisable.SetActive(!b);
            }
        }));
    }

    private IEnumerator FadeToBlackTime(float fadeSpeed, float opacity, Action<bool> callback) {
        while (canvasGroup.alpha < opacity - 0.000001) {
            var fadeAmount = canvasGroup.alpha + (fadeSpeed * Time.deltaTime);

            if (fadeAmount > opacity) {
                canvasGroup.alpha = opacity;

            } else {
                canvasGroup.alpha = fadeAmount;
                yield return null;
            }
        }
        
        callback.Invoke(true);
        yield return null;
    }

    private IEnumerator FadeToNormalTime(float speed, Action<bool> cal) {
        float fadeAmount;
        
        while (canvasGroup.alpha > 0) {
            fadeAmount = canvasGroup.alpha - (speed * Time.deltaTime);

            canvasGroup.alpha = fadeAmount;
            yield return null;
        }
        
        cal.Invoke(true);
    }
}
