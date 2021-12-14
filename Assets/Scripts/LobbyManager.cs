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

    void Start()
    {
        PhotonNetwork.JoinLobby();
        // Make sure player is in a room
        if (PhotonNetwork.CurrentRoom == null) return;

        // If player is in a room then chnange the text to the room name
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        // Update the player list
        UpdatePlayerList();

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

        int i = 0;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            //player.Value.CustomProperties["cursorIndex"] = i;

            PlayerLobbyItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            PhotonNetwork.LocalPlayer.CustomProperties["cursorIndex"] = i;
            i++;

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
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
        // Game can only be started by the master client and if there are at least 2 people
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playButton.SetActive(true);
        } else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("SelectSquad");
    }
}
