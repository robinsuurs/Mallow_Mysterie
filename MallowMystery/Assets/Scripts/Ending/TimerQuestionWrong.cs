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
    private bool hadWarning = false;
    private bool runEvent = false;
    
    public void startTimer(bool run) {
        runTimer = !run;
    }

    private void Update() {
        if (!runTimer || hadWarning) return;
        currentTime += Time.deltaTime;
        if (!(currentTime > maxTime)) return;
        hadWarning = true;
        unityEvent.Invoke();
    }

    public void End(bool value) {
        if (hadWarning && !value && !runEvent) {
            runEvent = true;
            ending.Invoke();
        }
    }
}
