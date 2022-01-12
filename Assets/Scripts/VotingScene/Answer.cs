using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Answer : MonoBehaviourPunCallbacks
{
    // Prefab to be created when voting and its intended parent
    public Vote playerVotePrefab;
    public Transform playerVoteParent; // AnswerContainer

    // Keeps track of object created when voting
    private GameObject myVote;
    [HideInInspector] public bool iHaveVoted;

    // Front and back side containers
    public GameObject frontSide;
    public GameObject backSide;
    public TMP_Text backSideScore;
    public TMP_Text backSideUserName;
    public Image backSideAvatar;


    // Answer text and vote tracker to give correct parent to vote object
    public TMP_Text answerText;
    private Player submittedBy;
    [HideInInspector] public int pointsThisRound;
    private VotingVotesTracker votesTracker;

    public void SetVotingStatus()
    {
        // Get the VotesTracker that is used to connect the squad with the votes
        votesTracker = FindObjectsOfType<VotingVotesTracker>()[0];

        // Link squad object to name 
        votesTracker.AddAnswerName(answerText.text, this);

        // Remove previous votes on Start if any
        RemovePreviousVotes();

        // Reset voting status
        iHaveVoted = false;
        pointsThisRound = 0;
    }

    public void RemovePreviousVotes()
    {
        foreach (Transform child in playerVoteParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddOrRemoveVote()
    {
        // You cannot vote for yourself
        if (submittedBy == PhotonNetwork.LocalPlayer) return;

        if (iHaveVoted)
        {
            // If I have voted then remove my vote
            PhotonNetwork.Destroy(myVote);
            iHaveVoted = false;
        }
        else
        {
            // If i have voted for another squad it removes that vote
            RemoveOtherVote();

            // If I haven't vote then add my vote
            object[] creator = new object[1];
            creator[0] = answerText.text;
            myVote = PhotonNetwork.Instantiate(playerVotePrefab.name, Vector3.zero, Quaternion.identity, 0, creator);
            iHaveVoted = true;
        }
    }

    private void RemoveOtherVote()
    {
        // Get all squads
        Dictionary<string, Answer> allAnswers = votesTracker.GetAllAnswerObjects();

        foreach (KeyValuePair<string, Answer> ans in allAnswers)
        {
            // If this is the squad that I have voted
            if (ans.Value.iHaveVoted == true)
            {
                // Remove my previous vote
                PhotonNetwork.Destroy(ans.Value.myVote);
                ans.Value.iHaveVoted = false;
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

    public TMP_Text GetAnswerName()
    {
        return answerText;
    }

    public GameObject GetSquadVotes()
    {
        return playerVoteParent.gameObject;
    }

    public void SetMyIHaveVoted(bool value)
    {
        iHaveVoted = value;
    }

    public void SetAnswerText(string _text)
    {
        answerText.text = _text;
    }

    public void SetSubmittedByPlayer(Player _player)
    {
        submittedBy = _player;
    }

    public Player GetSubmittedByPlayer()
    {
        return submittedBy;
    }

    public void ShowFront()
    {
        frontSide.SetActive(true);
        backSide.SetActive(false);
    }

    public void ShowBack(int score)
    {
        frontSide.SetActive(false);
        backSide.SetActive(true);
        backSideScore.text = score.ToString() + " Points";
        backSideUserName.text = submittedBy.NickName;
        backSideAvatar.sprite = votesTracker.GetAvatarImage((int)submittedBy.CustomProperties["playerAvatar"]);
    }
}
