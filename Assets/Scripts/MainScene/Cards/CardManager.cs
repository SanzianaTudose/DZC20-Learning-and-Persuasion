using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField]
    private List<Card> cardDeck;
    [SerializeField]
    private CardUsageManager cardUsageManager;

    private List<GameObject> cardObjects;
    // Holds a list of cards for each Expertise Area 
    private Dictionary<Card.ExpertiseAreas, List<Card>> organizedCards;
    // Keeps track of which card to draw next from each Expertise Area
    private Dictionary<Card.ExpertiseAreas, int> nextCard;

    private void Awake() {
        SetPrivateVars();
        DistributeCards();
    }

    // Called by the Submit button on AnswerBox
    public void OnClickSubmit() {
        if (!cardUsageManager.CanSubmit())
            return;

        DistributeCards();
    }

    public List<GameObject> GetCardObjects() {
        return cardObjects;
    }

    private void SetPrivateVars() {
        organizedCards = new Dictionary<Card.ExpertiseAreas, List<Card>>();
        nextCard = new Dictionary<Card.ExpertiseAreas, int>();
        foreach (Card card in cardDeck) {
            if (!organizedCards.ContainsKey(card.expertiseArea)) {
                organizedCards[card.expertiseArea] = new List<Card>();
                nextCard[card.expertiseArea] = 0;
            }

            organizedCards[card.expertiseArea].Add(card);
        }

        // Shuffle each card deck 
        foreach (Card.ExpertiseAreas expertise in organizedCards.Keys) 
            ShuffleCardList(organizedCards[expertise]);

        cardObjects = new List<GameObject>();
        foreach (Transform child in transform)
            cardObjects.Add(child.gameObject);
    }

    private void DistributeCards() {
        int curCardCount = 0;
        foreach (Card.ExpertiseAreas expertise in organizedCards.Keys) {
            GameObject cardObject = cardObjects[curCardCount++]; 
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            if (cardDisplay == null)
                Debug.LogError("CardManager.cs: no CardDisplay component found on child of CardsContainer.");

            if (nextCard[expertise] >= organizedCards[expertise].Count) {
                Debug.LogWarning("CardManager.cs: no more cards of Expertise Area: " + expertise.ToString());
                continue;
            }

            Card curCard = organizedCards[expertise][nextCard[expertise]++];
            cardDisplay.SetCard(curCard);
        }
    }

    #region Helper methods
    private void ShuffleCardList(List<Card> arg) {
        for (int i = arg.Count - 1; i > 0; i--) {
            int randIndex = Random.Range(0, i + 1);
            
            var temp = arg[i];
            arg[i] = arg[randIndex];
            arg[randIndex] = temp;
        }
    }
    #endregion
}
