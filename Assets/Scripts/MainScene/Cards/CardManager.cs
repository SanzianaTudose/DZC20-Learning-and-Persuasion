using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField]
    private List<Card> allCards;
    [SerializeField]
    private CardUsageManager cardUsageManager;

    [SerializeField] private List<GameObject> cardObjects;
    // Holds a list of cards for each Expertise Area 
    private Dictionary<Card.ExpertiseAreas, List<Card>> cardDeck;
    // Keeps track of which card to draw next from each Expertise Area
    private Dictionary<Card.ExpertiseAreas, int> nextCard;

    private void Awake() {
        SetPrivateVars();
        DistributeCards();
    }

    public List<GameObject> GetCardObjects() {
        return cardObjects;
    }

    private void SetPrivateVars() {
        cardDeck = new Dictionary<Card.ExpertiseAreas, List<Card>>();
        nextCard = new Dictionary<Card.ExpertiseAreas, int>();
        foreach (Card card in allCards) {
            if (!cardDeck.ContainsKey(card.expertiseArea)) {
                cardDeck[card.expertiseArea] = new List<Card>();
                nextCard[card.expertiseArea] = 0;
            }

            cardDeck[card.expertiseArea].Add(card);
        }

        // Shuffle each card deck 
        foreach (Card.ExpertiseAreas expertise in cardDeck.Keys) 
            ShuffleCardList(cardDeck[expertise]);
    }

    private void DistributeCards() {

        int curCardCount = 0;
        foreach (Card.ExpertiseAreas expertise in cardDeck.Keys) {
            GameObject cardObject = cardObjects[curCardCount++]; 
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            if (cardDisplay == null)
                Debug.LogError("CardManager.cs: no CardDisplay component found on child of CardsContainer.");

            if (nextCard[expertise] >= cardDeck[expertise].Count) {
                Debug.LogWarning("CardManager.cs: no more cards of Expertise Area: " + expertise.ToString());
                continue;
            }

            Card curCard = cardDeck[expertise][nextCard[expertise]++];
            cardDisplay.SetCard(curCard);
        }

        cardUsageManager.SetPrivateVars();
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
