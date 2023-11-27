using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Deduction/Question")]
public class Question : ScriptableObject {
    [SerializeField] private List<Answer> answers;
    [SerializeField] private string question;
    public string UID;

    public List<Answer> getAnswers() {
        return answers;
    }
    
    public string getAnswerGUIDBasedOnString(string answer) {
        return answers.Where(answerList => answerList.getAnswer().Equals(answer)).Select(answerList => answerList.UID).FirstOrDefault();
    }

    public string getAnswerStringBasedOnGUID(string GUID) {
        return answers.Where(answerList => answerList.UID.Equals(GUID)).Select(answerList => answerList.getAnswer()).FirstOrDefault();
    }
    
    private void OnValidate() {
        #if UNITY_EDITOR
        if (UID != "") return;
        UID = GUID.Generate().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
