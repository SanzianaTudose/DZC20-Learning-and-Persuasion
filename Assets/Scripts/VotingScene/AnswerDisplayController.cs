using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class AnswerDisplayController : MonoBehaviour {

    private Answer[] answerObjs;

    void Start() {
        // Get all answer objects
        answerObjs = gameObject.GetComponentsInChildren<Answer>();

        SetAnswers();
    }

    private void SetAnswers()
    {
        int textBoxCount = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Answer currentAns = answerObjs[textBoxCount];
            // TODO: handle players that didn't submit answer
            currentAns.SetAnswerText(player.CustomProperties["answer"].ToString());
            currentAns.SetSubmittedByPlayer(player);
            currentAns.SetVotingStatus();
            textBoxCount++;
        }
    }
}
