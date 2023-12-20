using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ListOfSprites : MonoBehaviour
{
#if UNITY_EDITOR
    private string spriteFolder = "CharacterImages";
    private string cutSceneImageFolder = "CutSceneImages";
    private bool spriteFolderExist = true;
    private bool cutSceneImageFolderExist = true;

    void OnValidate() {
        string fullPath = $"{Application.dataPath}/Resources/Sprites/{spriteFolder}";
        if (!System.IO.Directory.Exists(fullPath)) {            
            spriteFolderExist = false;
        }

        string fullPathCutScene = $"{Application.dataPath}/Resources/Sprites/{cutSceneImageFolder}";
        if (!System.IO.Directory.Exists(fullPathCutScene)) {            
            cutSceneImageFolderExist = false;
        }

        if (spriteFolderExist) {
            var folders = new string[]{$"Assets/Resources/Sprites/{spriteFolder}"};
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
            }        
        }

        if (cutSceneImageFolderExist) {
            var folders = new string[]{$"Assets/Resources/Sprites/{cutSceneImageFolder}"};
            var guids = AssetDatabase.FindAssets("t:Sprite", folders);

            var newCutSceneImages = new Sprite[guids.Length];

            bool mismatch;
            if (cutSceneImages == null) {
                mismatch = true;
                cutSceneImages = newCutSceneImages;
            } else {
                mismatch = newCutSceneImages.Length != cutSceneImages.Length;
            }

            for (int i = 0; i < newCutSceneImages.Length; i++) {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                newCutSceneImages[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                mismatch |= (i < cutSceneImages.Length && cutSceneImages[i] != newCutSceneImages[i]);
            }

            if (mismatch) {
                cutSceneImages = newCutSceneImages;
            } 
        }
        
    }
#endif
    [SerializeField] private Sprite[] characterSprites;
    
    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;
    
    public void CharacterSetter(string speakerLeft, string speakerRight) {
        var spriteToSetLeft = characterSprites.FirstOrDefault(sprite => sprite.name.Equals(speakerLeft));
        if (spriteToSetLeft != null) {
            leftImage.gameObject.SetActive(true);
            leftImage.sprite = spriteToSetLeft;
        } else {
            leftImage.gameObject.SetActive(false);
        }
        
        var spriteToSetRight = characterSprites.FirstOrDefault(sprite => sprite.name.Equals(speakerRight));
        if (spriteToSetRight != null) {
            rightImage.gameObject.SetActive(true);
            rightImage.sprite = spriteToSetRight;
        } else {
            rightImage.gameObject.SetActive(false);
        }  
    }

    private Sprite[] cutSceneImages;
    [SerializeField] private Image cutSceneImage;
    
    public void CutSceneImageSetter(string currentNodeCutSceneImageName) {
        if (currentNodeCutSceneImageName != null) {
            cutSceneImage.sprite =
                cutSceneImages.FirstOrDefault(sprite => sprite.name.Equals(currentNodeCutSceneImageName));
        } else {
            cutSceneImage.gameObject.SetActive(false);
        }
    }
}