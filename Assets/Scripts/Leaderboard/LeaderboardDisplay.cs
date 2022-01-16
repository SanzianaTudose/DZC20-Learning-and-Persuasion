using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardDisplay : MonoBehaviourPunCallbacks
{
    public TMP_Text roundText;
    private ExitGames.Client.Photon.Hashtable roomProps;
    private int currentRound;

    public DisplayMessage displayMessage;

    private void Start()
    {
        // Declare default values
        roomProps = new ExitGames.Client.Photon.Hashtable();
        currentRound = 1;

        // Get the current round
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("currentRound"))
        {
            currentRound = (int)PhotonNetwork.CurrentRoom.CustomProperties["currentRound"];
        }

        // Display the current round
        roundText.text = "Round " + currentRound.ToString();

        // Change the "currentRound" room custom property
        roomProps.Add("currentRound", currentRound + 1);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
    }

    public void StartNextRound()
    {
        if (currentRound == 3)
        {
            Debug.Log("Game Ended");
            displayMessage.DisplayNewMessage("Game Ended!");
            return;
        }

        PhotonNetwork.LoadLevel("SelectSquad");
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }
}