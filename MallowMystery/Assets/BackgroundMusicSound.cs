using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusicSound : MonoBehaviour {
    public AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;

    private void Start() {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
}
