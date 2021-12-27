using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class AnswerManager : MonoBehaviour {
    [SerializeField]
    private CardUsageManager cardUsageManager;

    [SerializeField]
    private TMP_InputField inputField;
    private List<string> answers = new List<string>();

    private int answerCount = 0;

    private const byte ANSWER_SUBMIT_EVENT = 0;

    // Called by the Submit button on AnswerBox
    public void OnClickSubmit() {
        if (!cardUsageManager.CanSubmit()) {
            print("you dont have enough cards to submit");
            return;
        }

        string answerText = GetAnswerText();

        // Raise Event to notify MasterClient that an answer has been submitted
        object[] data = new object[] { answerText };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // Set the Receivers to All to receive event on the local client
        PhotonNetwork.RaiseEvent(ANSWER_SUBMIT_EVENT, data, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);

        SetAnswerText("");
    }

    #region Photon Events-related methods
    private void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj) {
        if (obj.Code == ANSWER_SUBMIT_EVENT) {
            if (!PhotonNetwork.IsMasterClient) return;

            object[] data = (object[]) obj.CustomData;
            string answerText = (string) data[0];
            answers.Add(answerText);
            answerCount++;

            // Debugging answers
            string logMessage = "";
            logMessage += answerCount + " answer(s). ";
            foreach (var answer in answers)
                logMessage += answer + ";";
            print(logMessage);
        } 
    }
    #endregion

    #region UI-related methods
    public string GetAnswerText() {
        return inputField.text.ToString();
    }

    private void SetAnswerText(string newText) {
        inputField.text = "";
    }
    #endregion

}
