using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Deduction/Question")]
public class Question : ScriptableObject, IDataPersistence {
    [SerializeField] private List<Answer> answers;
    [SerializeField] private Answer chosenAnswer;
    public string question; //Needs to be public for dialogue
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

    public void setChosenAnswer(string answer) {
        chosenAnswer = answer == null ? null : answers.FirstOrDefault(answerList => answerList.getAnswer().Equals(answer));
    }

    public Answer getChosenAnswer() {
        return chosenAnswer;
    }
    
    private void OnValidate() {
        #if UNITY_EDITOR
        if (UID != "") return;
        UID = GUID.Generate().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    public void LoadData(GameData data) {
        string answerUID;
        data.questionAnswerDic.TryGetValue(UID, out answerUID);

        if (answerUID == null) {
            chosenAnswer = null;
        }
        else {
            foreach (var answer in answers.Where(answer => answer.UID.Equals(answerUID))) {
                chosenAnswer = answer;
            }
        }
    }

    public void SaveData(ref GameData data) {
        if (chosenAnswer != null) {
            data.questionAnswerDic[UID] = chosenAnswer.UID;
        } else {
            data.questionAnswerDic[UID] = null;
        }
        
    }
}
