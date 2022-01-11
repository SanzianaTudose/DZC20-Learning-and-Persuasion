using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class AnswerManager : MonoBehaviour {
    [SerializeField]
    private CardUsageManager cardUsageManager;
    
    private List<string> answers = new List<string>();

    private int answerCount = 0;

    // Photon variables
    private const byte ANSWER_SUBMIT_EVENT = 0;
    private ExitGames.Client.Photon.Hashtable myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    // UI fields
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text submitButtonText;

    // Called by the Submit button on AnswerBox
    public void OnClickSubmit() {
        if (!cardUsageManager.CanSubmit()) {
            // TODO: give visual feedback to the player
            print("you dont have enough cards to submit");
            return;
        }

        string answerText = GetAnswerText();

        // Add the submitted answer as a CustomProperty of the player
        myCustomProperties["answer"] = answerText;
        PhotonNetwork.LocalPlayer.SetCustomProperties(myCustomProperties);

        // Raise Event to notify MasterClient that an answer has been submitted
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // Set the Receivers to All to receive event on the local client
        PhotonNetwork.RaiseEvent(ANSWER_SUBMIT_EVENT, new object[] {}, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);

        // Set "submit" button text to "re-submit" to let player know they can change answer
        submitButtonText.SetText("re-submit");
    }

    // Called when timer ends, transitions players to VotingScene
    public void OnAnswerTimerEnd() {
        PhotonNetwork.LoadLevel("VotingScene");
    }

    #region Photon Events-related methods
    private void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj) {
        // Counts the number of answers submitted and transitions when all players have submitted
        if (obj.Code == ANSWER_SUBMIT_EVENT) {
            if (!PhotonNetwork.IsMasterClient) return;

            // Counts the number of answers submitted and transitions when all players have submitted
            if (obj.Code == ANSWER_SUBMIT_EVENT) {
                if (!PhotonNetwork.IsMasterClient) return;

                int answerCount = 0;
                foreach (var player in PhotonNetwork.PlayerList) {
                    var answer = player.CustomProperties["answer"];
                    if (answer != null)
                        answerCount++;
                }

                // If every player submitted, transition to VotingScene
                if (answerCount == PhotonNetwork.PlayerList.Length)
                    PhotonNetwork.LoadLevel("VotingScene");
            }

            // If every player submitted, transition to VotingScene
            if (answerCount == PhotonNetwork.PlayerList.Length)
                PhotonNetwork.LoadLevel("VotingScene");
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
