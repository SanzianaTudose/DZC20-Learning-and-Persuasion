using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class QuestionMarkController : MonoBehaviourPunCallbacks {
    [Header("UI Fields")]
    [SerializeField] private TMP_Text summaryText;
    [SerializeField] private TMP_Text boardSquadText;

    string pickedSquad, pickedCase;

    public void Start() {
        ExitGames.Client.Photon.Hashtable roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        pickedSquad = (string)roomProperties["pickedSquad"];
        pickedCase = (string)roomProperties["pickedCase"];

        boardSquadText.text = pickedSquad;
    }

    public void OnClickQuestionMark() {
        summaryText.SetText("Case summary: " + pickedCase);
    }
}
