using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class AnswerDisplayController : MonoBehaviour {

    public Answer[] answerObjs;

    void Start() {
        SetAnswers();
    }

    private void SetAnswers()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        for (int i = playerCount; i < answerObjs.Length; i++)
        {
            GameObject.Destroy(answerObjs[i].gameObject);
        }

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
