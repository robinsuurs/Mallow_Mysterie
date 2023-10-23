using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject levelManager;

    private void OnCollisionEnter(Collision other)
    {
        SceneSwitching ss = levelManager.GetComponent<SceneSwitching>();
        ss.LoadNextScene();
    }
}
