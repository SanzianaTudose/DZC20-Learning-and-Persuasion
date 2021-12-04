using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour {
    [SerializeField]
    private List<Card> cardDeck;

    // Holds a list of cards for each Expertise Area 
    private Dictionary<Card.ExpertiseAreas, List<Card>> organizedCards;
    private List<GameObject> cardObjects;

    private void Awake() {
        SetPrivateVars();
        DistributeCards();
    }

    private void Update() {
        // TODO: remove this, it's just for debugging
        if (Input.GetKeyDown("space")) {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }

    private void SetPrivateVars() {
        organizedCards = new Dictionary<Card.ExpertiseAreas, List<Card>>();
        foreach (Card card in cardDeck) {
            if (!organizedCards.ContainsKey(card.expertiseArea))
                organizedCards[card.expertiseArea] = new List<Card>();

            organizedCards[card.expertiseArea].Add(card);
        }

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

            Card curCard = organizedCards[expertise][Random.Range(0, organizedCards[expertise].Count)];
            cardDisplay.SetCard(curCard);
        }
    }
}
