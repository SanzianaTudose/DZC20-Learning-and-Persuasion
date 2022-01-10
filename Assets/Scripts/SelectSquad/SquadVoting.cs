using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SquadVoting : MonoBehaviourPunCallbacks
{
    // Prefab to be created when voting and its intended parent
    public SquadVote playerVotePrefab;
    public Transform playerVoteParent; // SquadVotes

    // Keeps track of object created when voting
    private GameObject myVote;
    public bool iHaveVoted;

    // Squad name and vote tracker to give correct parent to vote object
    public TMP_Text squadName; // SquadName
    private VotesTracker votesTracker;

    public void SetVotingStatus()
    {
        // Get the VotesTracker that is used to connect the squad with the votes
        votesTracker = FindObjectsOfType<VotesTracker>()[0];
        // Link squad object to name 
        votesTracker.AddSquadName(squadName.text, this);

        // Remove previous votes on Start if any
        RemovePreviousVotes();

        // Reset voting status
        iHaveVoted = false;
    }

    void RemovePreviousVotes()
    {
        foreach (Transform child in playerVoteParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddOrRemoveVote()
    {
        if (iHaveVoted)
        {
            // If I have voted then remove my vote
            PhotonNetwork.Destroy(myVote);
            iHaveVoted = false;
        } else
        {
            // If i have voted for another squad it removes that vote
            RemoveOtherVote();

            // If I haven't vote then add my vote
            object[] creator = new object[1];
            creator[0] = squadName.text;
            myVote = PhotonNetwork.Instantiate(playerVotePrefab.name, Vector3.zero, Quaternion.identity, 0 , creator);
            iHaveVoted = true;
        }
    }

    private void RemoveOtherVote()
    {
        // Get all squads
        Dictionary<string, SquadVoting> allSquads = votesTracker.GetAllSquadObjects();

        foreach (KeyValuePair<string, SquadVoting> squad in allSquads)
        {
            // If this is the squad that I have voted
            if (squad.Value.iHaveVoted == true)
            {
                // Remove my previous vote
                PhotonNetwork.Destroy(squad.Value.myVote);
                squad.Value.iHaveVoted = false;
                break;
            }
        }
    }

    public void AddObjectAsMyChild(GameObject vote)
    {
        // Sets the correct parent and dimensions for each vote
        // Method is called by the vote object itself
        vote.transform.SetParent(playerVoteParent.transform);
        vote.transform.localScale = Vector3.one;
        vote.transform.localPosition = Vector3.zero;
    }

    public TMP_Text GetSquadName()
    {
        return squadName;
    }

    public GameObject GetSquadVotes()
    {
        return playerVoteParent.gameObject;
    }

    public void SetMyIHaveVoted(bool value)
    {
        iHaveVoted = value;
    }
}
