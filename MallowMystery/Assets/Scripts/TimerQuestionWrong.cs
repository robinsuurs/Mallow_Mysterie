using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerQuestionWrong : MonoBehaviour {
    [SerializeField] private float currentTime;
    [SerializeField] private float maxTime;

    [SerializeField] private UnityEvent ending;

    private bool runTimer;
    
    public void startTimer(bool run) {
        runTimer = !run;
    }

    private void Update() {
        if (runTimer) {
            currentTime += Time.deltaTime;
            if (currentTime > maxTime) {
                //Open UI
                Debug.Log("Time Reached, EXPLODE");
            }
        }
    }

    public void End() {
        if (runTimer && currentTime > maxTime) {
            ending.Invoke();
        }
    }
}
