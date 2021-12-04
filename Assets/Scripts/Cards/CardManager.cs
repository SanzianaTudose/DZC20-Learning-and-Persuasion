using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    [SerializeField]
    private List<Card> cards;

    private List<GameObject> cardObjects;

    void Awake() {
        SetPrivateVars();
        DistributeCards();
    }

    private void SetPrivateVars() {
        cardObjects = new List<GameObject>();
        foreach (Transform child in transform)
            cardObjects.Add(child.gameObject);
    }

    private void DistributeCards() { 
        foreach (GameObject cardObject in cardObjects) {
            CardDisplay cardDisplay= cardObject.GetComponent<CardDisplay>();
            if (cardDisplay == null) {
                Debug.LogError("CardManager.cs: no CardDisplay component found on child of CardsContainer.");
            }

            cardDisplay.SetCard(cards[Random.Range(0, cards.Count - 1)]);
        }
    }
}
