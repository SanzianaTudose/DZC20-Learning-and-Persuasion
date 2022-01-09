using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class SquadPicker : MonoBehaviourPunCallbacks
{
    public TMP_Text[] squadNames;
    public SquadVoting[] squadVoting;

    private string[] availableSquads;
    private string[] usedSquads;
    public string[] chosenSquadNames;

    List<string> avSquadsList = new List<string>();
    List<string> usSquadsList = new List<string>();
    List<string> newUsedSquadsList = new List<string>();

    ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            SetAppropriateSquadNames();
        }
    }

    void SetAppropriateSquadNames()
    {
        availableSquads = (string[])PhotonNetwork.LocalPlayer.CustomProperties["availableSquads"];
        usedSquads = (string[])PhotonNetwork.LocalPlayer.CustomProperties["usedSquads"];

        // Convert available squads to a list
        foreach (string squad in availableSquads)
        {
            avSquadsList.Add(squad);
        }

        // Convert used squads to a list
        foreach (string squad in usedSquads)
        {
            usSquadsList.Add(squad);
        }

        chosenSquadNames = new string[squadNames.Length];

        // Pick squad name if there are enough available squads
        for (int i = 0; i < chosenSquadNames.Length; i++)
        {
            // If no more available squads pick one that has already been used
            if (avSquadsList.Count == 0)
            {
                chosenSquadNames[i] = PickUsedSquad();
            }
            //  Else get new squad name from available and assign it
            else
            {
                chosenSquadNames[i] = PickAvailableSquad();
            }

            // Remember which squad was used
            newUsedSquadsList.Add(chosenSquadNames[i]);
        }

        // Convert the other available squads into a string array
        string[] newAvlblSquads = new string[avSquadsList.Count];
        for (int i = 0; i < avSquadsList.Count; i++)
        {
            newAvlblSquads[i] = avSquadsList[i];
        }

        // Get all used squads
        newUsedSquadsList.AddRange(usSquadsList);

        // Convert the used squads into a string array
        string[] newUsedSquads = new string[newUsedSquadsList.Count];
        for (int i = 0; i < newUsedSquadsList.Count; i++)
        {
            newUsedSquads[i] = newUsedSquadsList[i];
        }


        // Set custom property
        roomProperties["availableSquads"] = newAvlblSquads;
        roomProperties["usedSquads"] = newUsedSquads;
        roomProperties["chosenSquads"] = chosenSquadNames;
        PhotonNetwork.LocalPlayer.SetCustomProperties(roomProperties);

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer.IsMasterClient)
        {
            string[] a = (string[])targetPlayer.CustomProperties["chosenSquads"];

            for (int i = 0; i < a.Length; i++)
            {
                squadNames[i].text = a[i];
            }

            SetSquadsVotingStatus();
        }
    }

    string PickAvailableSquad()
    {
        int rand = Random.Range(0, avSquadsList.Count);
        string toReturn = avSquadsList[rand];
        avSquadsList.RemoveAt(rand);

        return toReturn;
    }

    string PickUsedSquad()
    {
        int rand = Random.Range(0, usSquadsList.Count);
        string toReturn = usSquadsList[rand];
        usSquadsList.RemoveAt(rand);

        return toReturn;
    }

    void SetSquadsVotingStatus()
    {
        foreach (SquadVoting squad in squadVoting)
        {
            squad.SetVotingStatus();
        }
    }

    // Function for debugging
    public void GoToLobbyButton()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("InsideLobby");
        }
    }
}
