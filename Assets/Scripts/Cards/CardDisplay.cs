using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour {
    private Card card;

    [SerializeField]
    private TextMeshProUGUI wordText;
    [SerializeField]
    private TextMeshProUGUI expertiseText;

    void Start() {
        wordText.text = card.word;
        expertiseText.text = card.expertiseArea.ToString();
    }

    public void SetCard(Card newCard) {
        card = newCard;
    }
}
