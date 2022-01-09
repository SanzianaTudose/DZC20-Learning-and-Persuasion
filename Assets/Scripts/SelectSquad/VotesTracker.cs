using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class VotesTracker : MonoBehaviourPunCallbacks
{
    // All currently possible squads to vote for
    private Dictionary<string, SquadVoting> squads;

    // All possible player avatars
    public Sprite[] avatars;


    void Start()
    {
        // Initialize the squads
        squads = new Dictionary<string, SquadVoting>();
    }

    // Links a squad to the corresponding object
    public void AddSquadName(string name, SquadVoting squad)
    {
        squads.Add(name, squad);
    }

    // Returns the squad object corresponding to appropriate squad name
    public SquadVoting GetSquadObject(string name)
    {
        foreach (var squad in squads)
        {
            if (squad.Key.Equals(name)) return squad.Value;
        }

        return null;
    }

    // Returns the avatar image corresponding to an index
    public Sprite GetAvatarImage(int index)
    {
        return avatars[index];
    }
}
