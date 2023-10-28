using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class AnimationController : MonoBehaviour {
    [SerializeField] private PlayableAsset _playableAsset;
    private Camera _camera;
    private PlayableDirector _playableDirector;

    private void Start() {
        _camera = FindObjectsOfType<Camera>(true).FirstOrDefault(cam => cam.name.Equals("CutSceneCamera"));
        if ( GameObject.FindWithTag("AnimationImageShower") != null) {
            _playableDirector = GameObject.FindWithTag("AnimationImageShower").GetComponent<PlayableDirector>();
        }
        else {
            Debug.Log("Couldn't AnimationImageShower (Canvasses -> Cutscene is supposed to be on not off)");
        }
        
    }

    public void startAnimation() {
        Time.timeScale = 0;
        _camera.gameObject.SetActive(true);
        _playableDirector.playableAsset = _playableAsset;
        _playableDirector.Play();
    }

    public void endAnimation() {
        _camera.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
