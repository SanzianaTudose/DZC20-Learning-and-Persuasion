using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardDisplay : MonoBehaviourPunCallbacks
{
    public void StartNextRound()
    {
        PhotonNetwork.LoadLevel("SelectSquad");
    }
}