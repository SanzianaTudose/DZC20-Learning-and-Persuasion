using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

/* Attached to CaseIntroCanvas. Called by SquadPicker.cs when timer ends. 
 * Handles picking a Case for the input squad and transitioning from Squad voting to Case introduction. 
 * input: picked Squad
 */
public class CaseIntroController : MonoBehaviour {
    [Header("UI Fields")]
    [SerializeField] private GameObject squadVoteCanvas;
    [SerializeField] private TMP_Text squadText;
    [SerializeField] private TMP_Text caseText;
    [SerializeField] private GameObject continueButton;

    /*
        "Inclusive Design and Thoughtful Technology",
        "Games And Play",
        "Vitality",
        "Transforming Practices",
        "Crafting Wearable Senses",
        "New futures (connectivity in the home)",
        "Health",
        "Sensory Matters (sustainable food systems)",
        "Artifice - Artificial Intelligence",
     */

    // TODO: came up with a structure to hold the cases 

    public void startCaseIntro(string pickedSquad) {
        squadText.SetText(pickedSquad);

        // Transition UI
        squadVoteCanvas.SetActive(false);
        this.gameObject.SetActive(true);

        // Pick case for Squad
        string pickedCase = pickCaseforSquad(pickedSquad);
        caseText.SetText(pickedCase);

        // Only enable Continue button for MasterClient
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
            continueButton.SetActive(false);
    }

    public void OnClickContinue() {
        PhotonNetwork.LoadLevel("MainScene");
    }

    private string pickCaseforSquad(string pickedSquad) {
        // TODO: implement this
        return "lorem ipsum dolor sit amet";
    }
}
