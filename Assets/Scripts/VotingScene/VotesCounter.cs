using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class VotesCounter : MonoBehaviourPunCallbacks
{
    public AnswerDisplayController ansContainer;
    public TransitionToResults transitionObj;
    private Answer[] ansObjs;
    public void CountVotes()
    {
        ansObjs = ansContainer.GetComponentsInChildren<Answer>();
        int[] newPointsArray = new int[ansObjs.Length];

        for (int i = 0; i < ansObjs.Length; i++)
        {
            // Get player that submitted the answer
            Player submittedBy = ansObjs[i].GetSubmittedByPlayer();

            // Get number of votes for the answer
            int votes = ansObjs[i].GetSquadVotes().GetComponentsInChildren<Vote>().Length;

            // Save the points that the player should get
            int newPoints = GetNumberOfPoints(votes, 1);
            ansObjs[i].pointsThisRound = newPoints;

            // Add the points to the current points of the player
            AddPointsToPlayer(submittedBy, newPoints);

            newPointsArray[i] = newPoints;
        }

        transitionObj.StartRotatingAnswers();
    }

    public void GetLocalPoints()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["points"]);
    }


    private void AddPointsToPlayer(Player _player, int points)
    {
        ExitGames.Client.Photon.Hashtable pointsProps = new ExitGames.Client.Photon.Hashtable();

        if (_player.CustomProperties.ContainsKey("points"))
        {
            int currentPoints = (int)_player.CustomProperties["points"];
            pointsProps["points"] = points + currentPoints;
        } else
        {
            pointsProps["points"] = points;
        }

        _player.SetCustomProperties(pointsProps);
    }

    private int GetNumberOfPoints(int numberOfVotes, int multiplier)
    {
        int result = 0;

        if (numberOfVotes == 3) result = 1000;
        if (numberOfVotes == 2) result = 500;
        if (numberOfVotes == 1) result = 250;

        return result * multiplier;
    }
}
