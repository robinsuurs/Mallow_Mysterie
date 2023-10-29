using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour {
    private static Secret secret;
    [SerializeField] private AudioSource AudioSource;
    public void playMusic() {
        AudioSource.gameObject.SetActive(true);
    }

    private void stopMusic() {
        AudioSource.gameObject.SetActive(false);
    }

    private void Update() {
        if (!this.gameObject.activeSelf) {
            stopMusic();
        }
        else if (!AudioSource.gameObject.activeSelf) {
            playMusic();
        }
    }
}
