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
}