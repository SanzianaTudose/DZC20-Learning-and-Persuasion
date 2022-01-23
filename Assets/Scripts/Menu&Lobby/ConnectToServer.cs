using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject loadingPrompt;

    public void Connect() {
        loadingPrompt.SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("CreateAndJoinLobby");
    }
}
