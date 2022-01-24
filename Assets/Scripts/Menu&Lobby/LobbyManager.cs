using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_Text roomName;

    public List<PlayerLobbyItem> playerItemLists = new List<PlayerLobbyItem>();
    public PlayerLobbyItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;
    ExitGames.Client.Photon.Hashtable test = new ExitGames.Client.Photon.Hashtable();


    void Start()
    {
        PhotonNetwork.JoinLobby();
        // Make sure player is in a room
        if (PhotonNetwork.CurrentRoom == null) return;

        // If player is in a room then chnange the text to the room name
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        // Update the player list
        UpdatePlayerList();

        SetSquadNames();

    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("CreateAndJoinLobby");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    private void UpdatePlayerList()
    {
        // Remove old playerItems
        foreach (PlayerLobbyItem item in playerItemLists)
        {
            Destroy(item.gameObject);
        }
        playerItemLists.Clear();

        // Make sure player is in a room
        if (PhotonNetwork.CurrentRoom == null) return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            // Instantiate a player prefab item and set its' info
            PlayerLobbyItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                // Set cursorIndex for the localPlayer locally and globally 
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                hash.Add("cursorIndex", player.Key - 1);
                player.Value.SetCustomProperties(hash);

                // Applies local cursor changes for cursor
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemLists.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        // Game can only be started by the master client and if there are at least 1 people
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playButton.SetActive(true);
            SetSquadNames();
        } else
        {
            playButton.SetActive(false);
        }
    }

    void SetSquadNames()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("availableSquads")) return;

        // WARNING: if these names are changed => Case.cs will have errors
        string[] availableSquads =
            {
                "Inclusive Design and Thoughtful Technology",
                "Games And Play",
                "Vitality",
                "Transforming Practices",
                "Crafting Wearable Senses",
                "New Futures (connectivity in the home)",
                "Health",
                "Sensory Matters (sustainable food systems)",
                "Artifice - Artificial Intelligence",
            };
        string[] usedSquads = new string[0];

        ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();
        roomProperties.Add("availableSquads", availableSquads);
        roomProperties.Add("usedSquads", usedSquads);
        roomProperties.Add("currentRound", 1);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("SelectSquad");
        Destroy(FindObjectsOfType<DontDestroyAudio>()[0].gameObject);
    }
}
