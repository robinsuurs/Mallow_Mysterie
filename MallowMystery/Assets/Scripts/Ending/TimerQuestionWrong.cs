using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerQuestionWrong : MonoBehaviour {
    [SerializeField] private float currentTime;
    [SerializeField] private float maxTime;
    [SerializeField] private UnityEvent unityEvent;

    [SerializeField] private UnityEvent ending;

    private bool runTimer = false;
    private bool opendUI = false;
    
    public void startTimer(bool run) {
        runTimer = !run;
    }

    private void Update() {
        if (runTimer) {
            currentTime += Time.deltaTime;
            if (currentTime > maxTime && !opendUI) {
                unityEvent.Invoke();
                opendUI = true;
            }
        }
    }

    public void End() {
        if (runTimer && currentTime > maxTime) {
            ending.Invoke();
        }
    }
}
