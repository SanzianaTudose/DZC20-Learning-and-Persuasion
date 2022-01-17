using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class QuestionMarkController : MonoBehaviourPunCallbacks {
    [Header("UI Fields")]
    [SerializeField] private TMP_Text summaryText;

    public void OnClickQuestionMark() {
       ExitGames.Client.Photon.Hashtable roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;

        string pickedSquad = (string) roomProperties["pickedSquad"];
        string pickedCase = (string) roomProperties["pickedCase"];

        summaryText.SetText("Case summary for squad " + pickedSquad + ":\n\n" + pickedCase);
    }
}
