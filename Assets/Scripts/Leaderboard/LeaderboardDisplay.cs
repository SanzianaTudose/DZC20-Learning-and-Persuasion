using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class LeaderboardDisplay : MonoBehaviourPunCallbacks
{
    public TMP_Text roundText;
    private ExitGames.Client.Photon.Hashtable roomProps;
    private int currentRound;

    public DisplayMessage displayMessage;

    public DisplayPlayerPoints displayPlayerPointsPrefab;
    private Dictionary<int, DisplayPlayerPoints> allPlayerPoints;
    public GameObject playerPointsParent;

    private void Start()
    {
        DisplayPlayerPointsObjects();

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
    }

    private void DisplayPlayerPointsObjects()
    {
        allPlayerPoints = new Dictionary<int, DisplayPlayerPoints>();

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            DisplayPlayerPoints pointsObj = GameObject.Instantiate(displayPlayerPointsPrefab, Vector3.zero, Quaternion.identity, playerPointsParent.transform);
            pointsObj.SetUpPlayerPoints(player.Value);
            allPlayerPoints.Add(pointsObj.GetPlayerPoints(), pointsObj);
        }

        foreach (KeyValuePair<int, DisplayPlayerPoints> pointsObj in allPlayerPoints.OrderByDescending(key => key.Key))
        {
            pointsObj.Value.transform.SetParent(transform);
        }
        
    }

    public void StartNextRound()
    {
        if (currentRound == 3)
        {
            Debug.Log("Game Ended");
            displayMessage.DisplayNewMessage("Game Ended!");
            return;
        }

        // Change the "currentRound" room custom property
        roomProps.Add("currentRound", currentRound + 1);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);

        PhotonNetwork.LoadLevel("SelectSquad");
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }
}