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
    [SerializeField] private GameObject endingCanvasToEnableDisable;
    [SerializeField] private CanvasGroup endingCanvasGroup;
    [SerializeField] private UnityEvent booleanRun;
    private bool changingToBlack;

    private bool changing = false;

    public void FadeToBlackSequence(float fadeSpeed, float opacity, bool runEvent) {
        
        if (runEvent) {
            endingCanvasToEnableDisable.SetActive(true);
            StartCoroutine(FadeToBlackTime(fadeSpeed, opacity, endingCanvasGroup, b => {
                booleanRun.Invoke();
            }));
        } else {
            canvasToEnableDisable.SetActive(true);
            StartCoroutine(FadeToBlackTime(fadeSpeed, opacity, canvasGroup, b => {
                if (changing) return;
                changing = true;
            }));
        }
    }

    public void FadeToNormal(float fadeSpeed, float opacity, bool runEvent) {
          StartCoroutine(FadeToNormalTime(fadeSpeed, b => {
            if (b) {
                canvasToEnableDisable.SetActive(!b);
            }
        }));
    }

    private IEnumerator FadeToBlackTime(float fadeSpeed, float opacity, CanvasGroup canvasGroup, Action<bool> callback) {
        while (canvasGroup.alpha < opacity - 0.000001) {
            var fadeAmount = canvasGroup.alpha + (fadeSpeed * Time.deltaTime);

            if (fadeAmount > opacity) {
                canvasGroup.alpha = opacity;
                break;
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
