using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerManager : MonoBehaviour {
    [SerializeField]
    private GameObject inputField;
    private List<string> answers = new List<string>();

    // Called by the Submit button on AnswerBox
    public void OnClickSubmit() {
        TMP_InputField tmpInputField = inputField.GetComponent<TMP_InputField>();
        if (tmpInputField == null)
            Debug.LogError("AnswerManager.cs: TMP_InputField not found on inputField GameObject.");

        // Save answer and reset InputField text
        string answerText = tmpInputField.text;
        answers.Add(answerText);
        tmpInputField.text = "";

        foreach (string answer in answers)
            print(answer);
    }
}
