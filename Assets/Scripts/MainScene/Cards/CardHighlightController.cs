using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages card hihglight based on words in the answer text
public class CardHighlightController : MonoBehaviour {

    [SerializeField]
    private AnswerManager answerManager;
    // Holds pair of <cardWord, cardObject> for easy access
    private Dictionary<string, GameObject> cardDict;

    void Start() {
        SetPrivateVars();
    }

    void Update() {
        foreach (string word in cardDict.Keys) {
            if (answerManager.GetAnswerText().Contains(word))
                ToggleCardHighlight(cardDict[word], true);
            else
                ToggleCardHighlight(cardDict[word], false);
        }
    }

    private void SetPrivateVars() {
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
