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
    private string otbWord = null;

    public bool getHasOTB() {
        return hasOTB;
    }

    // If OTB has been drawn, returns the word on the OTB otherwise returns null
    public string getOTBWord() {
        return otbWord;
    }

    // Called when Player clicks to draw an OTB Card
    public void OnBackClick() {
        StartCoroutine(RotateOTBCard());
        hasOTB = true;

        // Choose random word from the OTB Decks
        otbWord = OTBCardDeck[Random.Range(0, OTBCardDeck.Count)].word;
        wordText.SetText(otbWord);
    }

    private IEnumerator RotateOTBCard()
    {
        for (float i = 0f; i <= 180f; i += 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);

            if (i == 90f)
            {
                cardFront.SetActive(true);
                cardBack.SetActive(false);
            }
            if (i > 90f)
            {
                cardFront.transform.rotation = Quaternion.Euler(0f, -1f * (180 - i), 0f);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
