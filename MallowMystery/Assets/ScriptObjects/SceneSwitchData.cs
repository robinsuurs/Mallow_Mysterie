using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptObjects {
    [CreateAssetMenu(menuName = "SceneSwitch/SceneSwitchData")]
    public class SceneSwitchData : ScriptableObject {
        [SerializeField] private string sceneName;
        public Vector3 playerSpawnLocation;
        
        public string getSceneName() {
            return sceneName;
        }
    }
}
