using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingScript : MonoBehaviour {
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float timeBetweenDarkAndText;
    [SerializeField] private float endingTextTimeShownLength;
    [SerializeField] private float statsTextTimeShownLength;
    private EndingStringList endingStringList;
    
    [SerializeField] private InputActionAsset input;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private GameObject imageObject;
    [SerializeField] private GameObject videoObject;
    [SerializeField] private TextMeshProUGUI endingText;
    [SerializeField] private TextMeshProUGUI lengthDuration;
    [SerializeField] private TextMeshProUGUI itemsCollected;
    [SerializeField] private Inventory inventory;

    private void OnEnable() {
        videoObject.GetComponent<VideoPlayer>().loopPointReached += afterVideo;
    }

    private void OnDisable() {
        videoObject.GetComponent<VideoPlayer>().loopPointReached -= afterVideo;
    }

    // public void FadeToBlack(string endingTextEnding) {
    //     input.Disable();
    //     endCanvas.enabled = true;
    //     SetText(endingTextEnding);
    //     StartCoroutine(FadeToBlackTime());
    // }

    public void testing(EndingStringList endingStringList) {
        this.endingStringList = endingStringList;
        input.Disable();
        endCanvas.enabled = true;
        SetText();
        StartCoroutine(FadeToBlackTime());
    }

    // private void SetText(string endingTextEnding) {
    //     endingText.text = endingTextEnding;
    //     lengthDuration.text += TimeSpan.FromSeconds(TimePlayedTrack.currentTimeRun).ToString(@"hh\:mm\:ss");
    //     itemsCollected.text += inventory.items.Count(item => item.hasBeenPickedUp).ToString();
    // }
    
    private void SetText() {
        lengthDuration.text += TimeSpan.FromSeconds(TimePlayedTrack.currentTimeRun).ToString(@"hh\:mm\:ss");
        itemsCollected.text += inventory.items.Count(item => item.hasBeenPickedUp).ToString();
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
        
        yield return new WaitForSeconds(timeBetweenDarkAndText);

        endingText.gameObject.SetActive(true);
        foreach (var text in endingStringList.getEndingScriptList()) {
            endingText.text = text;
            yield return new WaitForSeconds(endingTextTimeShownLength);
        }
        
        
        // yield return new WaitForSeconds(endingTextTimeShownLength);
        endingText.gameObject.SetActive(false);

        lengthDuration.gameObject.SetActive(true);
        itemsCollected.gameObject.SetActive(true);
        yield return new WaitForSeconds(statsTextTimeShownLength);
        lengthDuration.gameObject.SetActive(false);
        itemsCollected.gameObject.SetActive(false);
        
        videoObject.SetActive(true);
        videoObject.GetComponent<VideoPlayer>().Play();
        
        input.Enable();
    }

    private void afterVideo(VideoPlayer source) {
        Debug.Log("EndingLoop");
    }
}
