using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OTBCardController : MonoBehaviour
{
    [Header("Gameplay variables")]
    [SerializeField] private List<Card> OTBCardDeck;

    [Header("UI Fields")]
    [SerializeField] private GameObject cardFront;
    [SerializeField] private TMP_Text wordText;
    [SerializeField] private GameObject cardBack;

    private bool hasOTB = false;
    private string OTBword = null;

    public bool getHasOTB() {
        return hasOTB;
    }

    // If OTB has been drawn, returns the word on the OTB otherwise returns null
    public string getOTBWord() {
        return OTBword;
    }

    // Called when Player clicks to draw an OTB Card
    public void OnBackClick() {
        cardFront.SetActive(true);
        cardBack.SetActive(false);

        // Choose random word from the OTB Decks
        OTBword = OTBCardDeck[Random.Range(0, OTBCardDeck.Count)].word;
        wordText.SetText(OTBword);
    }
}
