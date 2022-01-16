using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages card highlight based on words in the answer text
public class CardUsageManager : MonoBehaviour {

    [SerializeField] private AnswerManager answerManager;
    [SerializeField] private OTBCardController otbController;
    [Header("Gameplay values")]
    [SerializeField] private int cardsNeeded = 3;

    [Header("UI Fields")]
    [SerializeField] private GameObject otbCardObject;

    // Holds pair of <cardWord, cardObject> for easy access
    private Dictionary<string, GameObject> cardDict;
    private int cardsUsed = 0;
    private bool hasUsedOTB = false;

    public bool CanSubmit() {
        return (cardsUsed >= cardsNeeded) && hasUsedOTB;
    }

    private void Update() {
        string answerText = answerManager.GetAnswerText().ToLower();

        // Highlight cards and compute new cardsUsed
        int newCardsUsed = 0;
        foreach (string word in cardDict.Keys) {
            if (answerText.Contains(word.ToLower())) {
                newCardsUsed++;
                ToggleCardHighlight(cardDict[word], true);
            } else
                ToggleCardHighlight(cardDict[word], false);
        }
        cardsUsed = newCardsUsed;

        // Handle OTB usage
        hasUsedOTB = true;
        if (otbController.getHasOTB()) {
            string otbWord = otbController.getOTBWord();
            
            if (answerText.Contains(otbWord.ToLower())) {
                hasUsedOTB = true;
                ToggleCardHighlight(otbCardObject, true);
                Debug.Log("otb highlight");
            } else {
                hasUsedOTB = false;
                ToggleCardHighlight(otbCardObject, false);
            }
        }
    }

    // Called after distributing the cards
    public void SetPrivateVars() {
        CardManager cardManager = GetComponent<CardManager>();
        if (cardManager == null)
            Debug.Log("CardHighlightController.cs: No CardManager component found.");

        List<GameObject> cardObjects = cardManager.GetCardObjects();

        cardDict = new Dictionary<string, GameObject>();
        foreach (GameObject card in cardObjects) {
            cardDict.Add(card.GetComponent<CardDisplay>().GetCardWord(), card);
        }
    }

    private void ToggleCardHighlight(GameObject cardObject, bool isHighlighted) {
        Color highlightColor = Color.white;
        if (isHighlighted)
            highlightColor = Color.yellow;

        cardObject.GetComponent<Image>().color = highlightColor;
    }
}
