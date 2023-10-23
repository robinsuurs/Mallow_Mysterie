using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GummySceneManager : MonoBehaviour
{
    public static GummySceneManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        level1_sceneSwitch,
        level2_sceneSwitch
    }
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
