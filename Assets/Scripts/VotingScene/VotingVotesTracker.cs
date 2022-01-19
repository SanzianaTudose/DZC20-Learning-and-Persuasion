using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class VotingVotesTracker : MonoBehaviourPunCallbacks
{
    public DisplayMessage displayMessage;

    // All currently possible squads to vote for
    private Dictionary<string, Answer> answers;

    // All possible player avatars
    public Sprite[] avatars;

    void Start()
    {
        // Initialize the squads
        answers = new Dictionary<string, Answer>();
    }

    // Links a squad to the corresponding object
    public void AddAnswerName(string name, Answer ans)
    {
        answers.Add(name, ans);
    }

    // Returns the squad object corresponding to appropriate squad name
    public Answer GetAnswerObject(string name)
    {
        foreach (var ans in answers)
        {
            if (ans.Key.Equals(name)) return ans.Value;
        }

        return null;
    }

    public Dictionary<string, Answer> GetAllAnswerObjects()
    {
        return answers;
    }

    // Returns the avatar image corresponding to an index
    public Sprite GetAvatarImage(int index)
    {
        return avatars[index];
    }
}
