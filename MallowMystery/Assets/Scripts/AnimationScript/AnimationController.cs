using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.RunTime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class AnimationController : MonoBehaviour {
    [SerializeField] private PlayableAsset _playableAsset;
    [SerializeField] private DialogueContainer _dialogueContainer;
    [SerializeField] private UnityEvent response;
    [SerializeField] private bool doSomehtingAfter;
    private Camera _camera;
    private PlayableDirector _playableDirector;

    private void Start() {
        _camera = FindObjectsOfType<Camera>(true).FirstOrDefault(cam => cam.name.Equals("CutSceneCamera"));
        if ( GameObject.FindWithTag("AnimationImageShower") != null) {
            _playableDirector = GameObject.FindWithTag("AnimationImageShower").GetComponent<PlayableDirector>();
        }
        else {
            Debug.Log("Couldn't find AnimationImageShower (Canvasses -> Cutscene is supposed to be on not off)");
        }
        
    }

    public void startAnimation() {
        if (_dialogueContainer == null || (_dialogueContainer != null && !_dialogueContainer.alreadyHadConversation)) {
            Time.timeScale = 0;
            _camera.gameObject.SetActive(true);
            _playableDirector.playableAsset = _playableAsset;
            _playableDirector.Play();
        } else if (doSomehtingAfter) {
            responseActivate();
        }
    }

    public void endAnimation() {
        _camera.gameObject.SetActive(false);
        Time.timeScale = 1;
        if (doSomehtingAfter) {
            responseActivate();
        }
    }

    private void responseActivate() {
        response.Invoke();
    }
}
