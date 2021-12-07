using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
        Debug.Log("Successfully joined room: " + joinInput.text);
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
