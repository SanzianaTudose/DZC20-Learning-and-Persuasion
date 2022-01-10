using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

// ~ Super quick demo to see if it works ~
// Displays player answers in VotingScene
public class AnswerDisplayController : MonoBehaviour {
    [SerializeField]
    private List<TMP_Text> answerTextBoxes;

    void Start() {
        int textBoxCount = 0;
        foreach (var player in PhotonNetwork.PlayerList) {
            // TODO: handle players that didn't submit answer
            answerTextBoxes[textBoxCount].text = player.CustomProperties["answer"].ToString();
            textBoxCount++;
        }
    }
}
