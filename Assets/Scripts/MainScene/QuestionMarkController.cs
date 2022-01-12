using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuestionMarkController : MonoBehaviour {
    

    public void OnClickQuestionMark() {
        ExitGames.Client.Photon.Hashtable roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;

        string pickedSquad = (string) roomProperties["pickedSquad"];
        string pickedCase = (string) roomProperties["pickedCase"];

        Debug.Log(pickedSquad + "  " + pickedCase);
    }
    
}
