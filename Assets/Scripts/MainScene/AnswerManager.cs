using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AnswerManager : MonoBehaviour {
    [SerializeField]
    private CardUsageManager cardUsageManager;

    [SerializeField]
    private TMP_InputField inputField;
    private List<string> answers = new List<string>();

    // Called by the Submit button on AnswerBox
    public void OnClickSubmit() {
        if (!cardUsageManager.CanSubmit()) {
            print("you dont have enough cards to submit.");
            return;
        }

        string answerText = GetAnswerText();
        answers.Add(answerText);
        SetAnswerText("");
    }

    public string GetAnswerText() {
        return inputField.text.ToString();
    }

    private void SetAnswerText(string newText) {
        inputField.text = "";
    }
}
