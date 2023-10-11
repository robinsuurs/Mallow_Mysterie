using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public void LoadNextScene()
    {
        // TODO: Disable player movement
        // TODO: Play fade animation
        StartCoroutine(LoadNextSceneAfterSeconds(3));
    }

    IEnumerator LoadNextSceneAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
