using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dialogue.Editor.Graph {
    public class QuestionAnswerRetriever {
        private string Questions = "Resources/QuestionAnswers/Questions";
        private string Answers = "Resources/QuestionAnswers/Answers";
        
        public List<Questions> questionNames;
        public List<Answers> answersList;

        public QuestionAnswerRetriever() {
            RetrieveQuestionDataNames();
            RetrieveAnswersDataNames();
        }

        public void RetrieveQuestionDataNames() {
            var folders = new string[]{$"Assets/{Questions}"};
            var guids = AssetDatabase.FindAssets("t:Question", folders);

            var newQuestionData = new Object[guids.Length];

            List<Questions> namesOfQuestions = new List<Questions>();
            for (int i = 0; i < newQuestionData.Length; i++) {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                newQuestionData[i] = AssetDatabase.LoadAssetAtPath<Object>(path);
                string question = GetFieldValue<string>(newQuestionData[i], "question");
                string questionUID = GetFieldValue<string>(newQuestionData[i], "UID");
                
                namesOfQuestions.Add(new Questions(question, questionUID));
            }

            this.questionNames = namesOfQuestions;
        }
        
        public void RetrieveAnswersDataNames() {
            var folders = new string[]{$"Assets/{Answers}"};
            var guids = AssetDatabase.FindAssets("t:Answer", folders);

            var newAnswerData = new Object[guids.Length];

            List<Answers> namesOfAnswers = new List<Answers>();
            for (int i = 0; i < newAnswerData.Length; i++) {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                newAnswerData[i] = AssetDatabase.LoadAssetAtPath<Object>(path);
                string answer = GetFieldValue<string>(newAnswerData[i], "answer");
                string answerUID = GetFieldValue<string>(newAnswerData[i], "UID");
                
                namesOfAnswers.Add(new Answers(answer, answerUID));
            }

            this.answersList = namesOfAnswers;
        }
        
        //https://stackoverflow.com/questions/33684027/getting-property-and-field-values-from-an-object-using-reflection-and-using-a-st
        public System.Object GetFieldValue(System.Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                FieldInfo info = type.GetField(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj);
            }
            return obj;
        }

    // so we can use reflection to access the object properties
        public T GetFieldValue<T>(Object obj, String name)
        {
            System.Object retval = GetFieldValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public string getAnswerConnectedUID(string answer) {
            return answersList.Where(a => a.answer.Equals(answer)).Select(a => a.answerUID).FirstOrDefault();
        }
        
        public string getAnswerConnectedAnswerFromUID(string answerUID) {
            return answersList.Where(a => a.answerUID.Equals(answerUID)).Select(a => a.answer).FirstOrDefault();
        }
        
        public string getQuesitonConnectedUID(string question) {
            return questionNames.Where(a => a.question.Equals(question)).Select(a => a.questionUID).FirstOrDefault();
        }
        
        public string getQuestionConnectedQuestionFromUID(string questionUID) {
            return questionNames.Where(a => a.questionUID.Equals(questionUID)).Select(a => a.question).FirstOrDefault();
        }
    }
}