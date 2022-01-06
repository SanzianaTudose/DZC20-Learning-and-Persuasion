using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SquadVoting : MonoBehaviourPunCallbacks
{
    public SquadVote playerVotePrefab;
    public Transform playerVoteParent;

    private GameObject myVote;
    private bool iHaveVoted;

    public TMP_Text squadName;
    private VotesTracker votesTracker;

    void Start()
    {
        votesTracker = FindObjectsOfType<VotesTracker>()[0];
        votesTracker.AddSquadName(squadName.text, this);

        // Remove previous votes on Start if any
        RemovePreviousVotes();

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
            // If I haven't vote then add my vote
            object[] creator = new object[1];
            creator[0] = squadName.text;
            myVote = PhotonNetwork.Instantiate(playerVotePrefab.name, Vector3.zero, Quaternion.identity, 0 , creator);
            iHaveVoted = true;
        }
    }
    public void AddObjectAsMyChild(GameObject vote)
    {
        vote.transform.SetParent(playerVoteParent.transform);
        vote.transform.localScale = Vector3.one;
        vote.transform.localPosition = Vector3.zero;
    }
}
