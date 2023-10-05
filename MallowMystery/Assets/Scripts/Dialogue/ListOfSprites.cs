using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ListOfSprites : MonoBehaviour
{
#if UNITY_EDITOR
    public string spriteFolder = "CharacterImages";

    void OnValidate() {
        string fullPath = $"{Application.dataPath}/{spriteFolder}";
        if (!System.IO.Directory.Exists(fullPath)) {            
            return;
        }

        var folders = new string[]{$"Assets/{spriteFolder}"};
        var guids = AssetDatabase.FindAssets("t:Sprite", folders);

        var newSprites = new Sprite[guids.Length];

        bool mismatch;
        if (characterSprites == null) {
            mismatch = true;
            characterSprites = newSprites;
        } else {
            mismatch = newSprites.Length != characterSprites.Length;
        }

        for (int i = 0; i < newSprites.Length; i++) {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            newSprites[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            mismatch |= (i < characterSprites.Length && characterSprites[i] != newSprites[i]);
        }

        if (mismatch) {
            characterSprites = newSprites;
            Debug.Log($"{name} sprite list updated.");
        }        
    }
#endif
    public Sprite[] characterSprites;
    
    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;
    
    public void CharacterSetter(string speakerLeft, string speakerRight) {
        if (speakerLeft != "") {
            leftImage.gameObject.SetActive(true);
            foreach (var sprite in characterSprites) {
                if (!sprite.name.Equals(speakerLeft)) continue;
                leftImage.sprite = sprite;
                break;
            }
        } else {
            leftImage.gameObject.SetActive(false);
        }
        if (speakerRight != "") {
            rightImage.gameObject.SetActive(true);
            foreach (var sprite in characterSprites) {
                if (!sprite.name.Equals(speakerRight)) continue;
                rightImage.sprite = sprite;
                break;
            }
        } else {
            rightImage.gameObject.SetActive(false);
        }
    }
}