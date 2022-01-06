using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class VotesTracker : MonoBehaviourPunCallbacks
{
    private Dictionary<string, SquadVoting> squads;
    public Sprite[] avatars;


    void Start()
    {
        squads = new Dictionary<string, SquadVoting>();
    }

    public void AddSquadName(string name, SquadVoting squad)
    {
        squads.Add(name, squad);
    }

    public SquadVoting GetSquadObject(string name)
    {
        foreach (var squad in squads)
        {
            if (squad.Key.Equals(name)) return squad.Value;
        }

        return null;
    }

    public Sprite GetAvatarImage(int index)
    {
        return avatars[index];
    }
}
