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

            // Get round
            int round = (int)PhotonNetwork.CurrentRoom.CustomProperties["currentRound"];


            // Save the points that the player should get
            int newPoints = GetNumberOfPoints(votes, round, submittedBy);
            ansObjs[i].pointsThisRound = newPoints;

            if (submittedBy == PhotonNetwork.LocalPlayer)
            {
                // Add the points to the current points of the player
                AddPointsToPlayer(submittedBy, newPoints);
            }

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

    private int GetNumberOfPoints(int numberOfVotes, int multiplier, Player _submittedBy)
    {
        if (numberOfVotes < 0) numberOfVotes = 0;
        if (multiplier < 1) multiplier = 1;

        int result = 250 * numberOfVotes;

        // Get appropriate amount of points whether the player used the OTB card
        int pointsForOTB = 0;
        if (_submittedBy.CustomProperties.ContainsKey("usedOTB"))
        {
            if ((bool)_submittedBy.CustomProperties["usedOTB"])
            {
                pointsForOTB = 100;
            }
        }

        return result * multiplier + pointsForOTB;
    }
}
