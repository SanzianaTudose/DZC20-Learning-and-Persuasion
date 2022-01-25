using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ReadyButtonController : MonoBehaviour {
    [SerializeField] TMP_Text readyButtonText;

    // Photon variables
    private const byte READY_EVENT = 1;
    private ExitGames.Client.Photon.Hashtable myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    public void OnClickReady() {
        readyButtonText.text = "Waiting...";

        // Add the ready status as a CustomProperty of the player
        myCustomProperties["ready"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(myCustomProperties);

        // Raise Event to notify MasterClient that an answer has been submitted
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // Set the Receivers to All to receive event on the local client
        PhotonNetwork.RaiseEvent(READY_EVENT, new object[] { }, raiseEventOptions, ExitGames.Client.Photon.SendOptions.SendReliable);
    }

    #region Photon Events-related methods
    private void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj) {
        // Counts the number of ready players and transitions 
        if (obj.Code == READY_EVENT) {
            if (!PhotonNetwork.IsMasterClient) return;

            int readyCount = 0;
            foreach (var player in PhotonNetwork.PlayerList) {
                if (player.CustomProperties["ready"] != null) {
                    var status = (bool)player.CustomProperties["ready"];
                    if (status)
                        readyCount++;
                }
            }

            // If every player is ready, transition to SelectSquad
            if (readyCount == PhotonNetwork.PlayerList.Length)
                PhotonNetwork.LoadLevel("SelectSquad");
        }
    }
    #endregion
}
