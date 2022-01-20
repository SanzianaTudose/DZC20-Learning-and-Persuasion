using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class CardDisplay : MonoBehaviour {
    private Card card;

    [SerializeField]
    private TextMeshProUGUI wordText;
    [SerializeField]
    private TextMeshProUGUI expertiseText;

    public void SetCard(Card newCard) {
        card = newCard;
        UpdateUI();
    }

    public string GetCardWord() {
        return wordText.text.ToString();
    }

    private void UpdateUI() {
        wordText.text = card.word;
        
        string expertiseString = card.expertiseArea.ToString();
        expertiseString = Regex.Replace(expertiseString, "(\\B[A-Z])", " $1");
        
        expertiseText.text = expertiseString;
    }
}
