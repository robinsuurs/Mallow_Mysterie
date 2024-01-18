using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private AudioClip _walkHard;
    [SerializeField] private AudioClip _walkSoft;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] public bool inside;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = inside ? _walkHard : _walkSoft;
        _audioSource.PlayOneShot(clip);
    }
}
