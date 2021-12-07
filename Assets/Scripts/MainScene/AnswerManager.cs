using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AnswerManager : MonoBehaviour {
    [SerializeField]
    private TMP_InputField inputField;
    private List<string> answers = new List<string>();

    // Called by the Submit button on AnswerBox
    public void OnClickSubmit() {
        string answerText = GetAnswerText();
        answers.Add(answerText);
        SetAnswerText("");

        foreach (string answer in answers)
            print(answer);
    }

    public string GetAnswerText() {
        return inputField.text.ToString();
    }

    private void SetAnswerText(string newText) {
        inputField.text = "";
    }
}
