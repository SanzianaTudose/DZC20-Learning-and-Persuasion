using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class SquadPicker : MonoBehaviourPunCallbacks
{
    public SquadVoting[] squadVoting; // All squad voting objects
    private TMP_Text[] squadNames; // All text objects

    private string[] availableSquads;
    private string[] usedSquads;
    private string[] chosenSquadNames;

    List<string> avSquadsList = new List<string>();
    List<string> usSquadsList = new List<string>();
    List<string> newUsedSquadsList = new List<string>();

    ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();

    [SerializeField] private CaseIntroController caseIntroController;

    bool casePresented;

    private void Start()
    {
        casePresented = false;
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerAvatar"))
        {
            ExitGames.Client.Photon.Hashtable _properties = new ExitGames.Client.Photon.Hashtable();
            _properties["playerAvatar"] = 0;
            PhotonNetwork.LocalPlayer.SetCustomProperties(_properties);
        }

        GetParentsAndTextObjects();

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            SetAppropriateSquadNames();
        }
    }

    void SetAppropriateSquadNames()
    {
        availableSquads = (string[])PhotonNetwork.CurrentRoom.CustomProperties["availableSquads"];
        usedSquads = (string[])PhotonNetwork.CurrentRoom.CustomProperties["usedSquads"];

        // Convert available squads to a list
        foreach (string squad in availableSquads)
        {
            if (squad != "") avSquadsList.Add(squad);
        }

        // Convert used squads to a list
        foreach (string squad in usedSquads)
        {
            if (squad != "") usSquadsList.Add(squad);
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
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }

    private void GetParentsAndTextObjects()
    {
        int n = squadVoting.Length;
        squadNames = new TMP_Text[n];

        for (int i = 0; i < n; i++)
        {
            squadNames[i] = squadVoting[i].GetSquadName();
        }
    }

    private string PickAvailableSquad()
    {
        int rand = Random.Range(0, avSquadsList.Count);
        string toReturn = avSquadsList[rand];
        avSquadsList.RemoveAt(rand);

        return toReturn;
    }

    private string PickUsedSquad()
    {
        int rand = Random.Range(0, usSquadsList.Count);
        string toReturn = usSquadsList[rand];
        usSquadsList.RemoveAt(rand);

        return toReturn;
    }

    private void SetSquadsVotingStatus()
    {
        foreach (SquadVoting squad in squadVoting)
        {
            squad.SetVotingStatus();
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("chosenSquads"))
        {
            string[] _chosenSquads = (string[])PhotonNetwork.CurrentRoom.CustomProperties["chosenSquads"];

            for (int i = 0; i < _chosenSquads.Length; i++)
            {
                squadNames[i].text = _chosenSquads[i];
            }

            SetSquadsVotingStatus();
        }

        if (propertiesThatChanged.ContainsKey("pickedSquad") && !casePresented)
        {
            casePresented = true;
            caseIntroController.startCaseIntro((string)PhotonNetwork.CurrentRoom.CustomProperties["pickedSquad"]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public string CountVotes()
    {
        int maxVotes = -1;
        int maxCount = 1;
        int currentVotes;

        // Find number of votes that is maximal across all squads
        foreach (SquadVoting item in squadVoting)
        {
            // Get number of votes of current item
            currentVotes = item.GetSquadVotes().transform.childCount;

            if (currentVotes > maxVotes)
            {
                maxVotes = currentVotes;
                maxCount = 1;
            } else if (currentVotes == maxVotes)
            {
                maxCount++;
            }
        }

        // Variables for finding all names of squads with maximum votes
        string[] possibleSquads = new string[maxCount];
        int i = 0;

        foreach (SquadVoting item in squadVoting)
        {
            // Get number of votes of current item
            currentVotes = item.GetSquadVotes().transform.childCount;

            // If current item has maximum votes save its name
            if (currentVotes == maxVotes)
            {
                possibleSquads[i] = item.GetSquadName().text;
                i++;
            }
        }

        // Choose a random squad with maximum votes
        int rand = Random.Range(0, possibleSquads.Length);
        string chosenSquad = possibleSquads[rand];
            

        // Remove chosen squad so that it cannot be played again
        usedSquads = (string[])PhotonNetwork.CurrentRoom.CustomProperties["usedSquads"];
        for (int j = 0; j < usedSquads.Length; j++)
        {
            if (usedSquads[j] == chosenSquad)
            {
                usedSquads[j] = "";
                break;
            }
        }
        ExitGames.Client.Photon.Hashtable _roomProperties = new ExitGames.Client.Photon.Hashtable();
        _roomProperties.Add("usedSquads", usedSquads);
        PhotonNetwork.CurrentRoom.SetCustomProperties(_roomProperties);

        return chosenSquad;

    }

    // Method called when timer ends, counts votes and starts Case Introduction
    public void OnVoteEnd() {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable roomProps = new ExitGames.Client.Photon.Hashtable();
            roomProps.Add("pickedSquad", CountVotes());
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
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
