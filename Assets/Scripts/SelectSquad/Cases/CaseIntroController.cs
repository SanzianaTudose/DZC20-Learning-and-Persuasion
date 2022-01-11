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

    [Header("Gameplay Variables")]
    [SerializeField] private List<Case> allCases;

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
        List<Case> possibleCases = new List<Case>();

        foreach (Case it in allCases) {
            if (it.getSquadString() == pickedSquad)
                possibleCases.Add(it);
        }

        Case pickedCase = possibleCases[Random.Range(0, possibleCases.Count - 1)];
        return pickedCase.caseText;
    }
}
