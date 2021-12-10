using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    private void Start()
    {
        if (PhotonNetwork.NickName != null)
        {
            usernameInput.text = PhotonNetwork.NickName;
        }
    }

    public void CreateRoom()
    {
        if (_IsValidUserAndRoomName(createInput.text))
        {
            PhotonNetwork.NickName = usernameInput.text;
            PhotonNetwork.CreateRoom(createInput.text, new RoomOptions() { MaxPlayers = 8, BroadcastPropsChangeToAll = true }); ;
        }
    }

    public void JoinRoom()
    {
        if (_IsValidUserAndRoomName(joinInput.text))
        {
            PhotonNetwork.NickName = usernameInput.text;
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    private bool _IsValidUserAndRoomName(string text)
    {
        if (usernameInput.text.Length >= 1)
        {
            if (text.Length >= 1)
            {   
                PhotonNetwork.NickName = usernameInput.text;
                return true;
            } else
            {
                Debug.Log("Invalid room name!");
                return false;
            }
        } else
        {
            Debug.Log("Invalid username!");
            return false;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("InsideLobby");
        Debug.Log("Successfully joined room: " + PhotonNetwork.CurrentRoom.Name);
    }

    // Called if room that you want to create already exists
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    // Called if room that you want to join does not exist
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
}
