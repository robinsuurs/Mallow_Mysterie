using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerQuestionWrong : MonoBehaviour {
    [SerializeField] private float currentTime;
    [SerializeField] private float maxTime;
    [SerializeField] private GameEventStandardAdd standardEvent;

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
                standardEvent.Raise();
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
