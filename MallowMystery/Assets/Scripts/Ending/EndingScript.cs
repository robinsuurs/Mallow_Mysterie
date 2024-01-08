using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingScript : MonoBehaviour {
    
    [SerializeField] private float timeBetweenDarkAndText;
    [SerializeField] private float timeBetweenFadeOutFadeIn;
    [SerializeField] private float textFadeSpeed;
    private List<string> endingStringList;

    [SerializeField] private GameObject videoObject;
    [SerializeField] private TextMeshProUGUI endingNumber;
    [SerializeField] private TextMeshProUGUI lengthDuration;
    [SerializeField] private TextMeshProUGUI itemsCollected;
    [SerializeField] private Inventory inventory;
    [SerializeField] private int amountOfEndings;
    [SerializeField] private GameObject iconClick;

    private bool running = false;
    private int state = 0;
    private int stringListNumber = 0;

    [SerializeField] private List<TextMeshProUGUI> endingText;
    [SerializeField] private List<TextMeshProUGUI> stats;

    private void OnEnable() {
        videoObject.GetComponent<VideoPlayer>().loopPointReached += afterVideo;
    }

    private void OnDisable() {
        videoObject.GetComponent<VideoPlayer>().loopPointReached -= afterVideo;
    }

    private void Start() {
        SetText();
        StartCoroutine(EndRoutine());
    }
    
    private void SetText() {
        EndingStringList endList = DataPersistenceManager.instance.GetEndingStringList();
        endingStringList = endList.getEndingScriptList();
        endingNumber.text = "Ending " + endList.getEndingNumber() + "/" + amountOfEndings;
        lengthDuration.text += TimeSpan.FromSeconds(TimePlayedTrack.currentTimeRun).ToString(@"hh\:mm\:ss");
        itemsCollected.text += inventory.items.Count(item => item.hasBeenPickedUp).ToString();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !running) {
            iconClick.SetActive(false);
            running = true;
            switch (state) {
                case 0:
                    iconClick.SetActive(false);
                    StartCoroutine(OpacityTimeFadeInOut(endingText, endingText, endingStringList, textFadeSpeed, timeBetweenFadeOutFadeIn,
                        b => { iconClick.SetActive(true); }));
                    break;
                case 1:
                    iconClick.SetActive(false);
                    StartCoroutine(OpacityTimeFadeInOut(endingText, stats, null, textFadeSpeed, timeBetweenFadeOutFadeIn,
                        b => { iconClick.SetActive(true); }));
                    break;
                case 2:
                    StartCoroutine(OpacityTimeFadeInOut(stats, null, null, textFadeSpeed, timeBetweenFadeOutFadeIn, b => {
                        if (b) {
                            videoObject.SetActive(true);
                            videoObject.GetComponent<VideoPlayer>().Play();
                        }
                    }));

                    running = false;
                    break;
            }
        }
    }

    private IEnumerator EndRoutine() {
        running = true;
        yield return new WaitForSeconds(timeBetweenDarkAndText);
        
        StartCoroutine(OpacityTimeFadeInOut(null, endingText, endingStringList, textFadeSpeed, timeBetweenDarkAndText, b => { iconClick.SetActive(true); }));
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator OpacityTimeFadeInOut(List<TextMeshProUGUI> fadeOut, List<TextMeshProUGUI> fadeIn, List<string> ending, float fadeSpeed, float waitBetween, Action<bool> callback) {
        Color objectColor;
        float fadeAmount;

        if (fadeOut != null) {
            objectColor = fadeOut[0].color;
            
            while (objectColor.a > 0) {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                foreach (var text in fadeOut) {
                    text.color = objectColor;
                }
                yield return null;
            }
        }

        if (ending != null) {
            endingText[0].text = endingStringList[stringListNumber];
            stringListNumber++;
            if (stringListNumber == endingStringList.Count) {
                state++;
            }
        } else {
            state++;
        }
        
        yield return new WaitForSeconds(waitBetween);
        
        if (fadeIn != null) {
            objectColor = fadeIn[0].color;
            
            while (objectColor.a < 1) {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            
                foreach (var text in fadeIn) {
                    text.color = objectColor;
                }
                yield return false;
            }
        }

        running = false;
        callback(true);
        yield return true;
    }

    private void afterVideo(VideoPlayer source) {
        SceneManager.LoadScene("MainMenu");
    }
}
