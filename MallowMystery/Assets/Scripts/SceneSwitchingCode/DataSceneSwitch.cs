using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptObjects {
    [CreateAssetMenu(menuName = "SceneSwitch/SceneSwitchData")]
    public class SceneSwitchData : ScriptableObject {
        public string sceneName;
        public string playerSpawnLocationName;
    }
}
