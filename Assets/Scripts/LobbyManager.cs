using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_Text roomName;

    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            roomName.text = "NoName";
        } else
        {
            roomName.text = PhotonNetwork.CurrentRoom.Name;
        }
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("CreateAndJoinLobby");
    }
}
