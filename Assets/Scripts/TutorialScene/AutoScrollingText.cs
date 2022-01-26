using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoScrollingText : MonoBehaviour
{
    // Text object, speed and position
    public TMP_Text myText;

    // Text selection color and variable to store it as HEX value
    public Color textSelectionColor;
    private string myColorHex;

    // Variables to keep track of which text is selected
    private string remainingText;
    private string selectedText;

    // Keep track of time passed since last update of text selection
    private float lastCutTimeDelta;

    public Rigidbody2D rb;

    void Start()
    {
        // Set variables
        remainingText = myText.text;
        selectedText = "";
        myColorHex = "#" + ColorUtility.ToHtmlStringRGB(textSelectionColor);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, 1, 0);
    }

    void Update()
    {
        // If text object is at the end stop moving it
        if (myText.transform.localPosition.y > 1400f)
        {
            Destroy(rb);
        }

        float timeNeededToPass = 0.055f;
        // If enough time has passed update selection text
        if (lastCutTimeDelta > timeNeededToPass && remainingText.Length > 1)
        {
            // Update selection text
            selectedText += remainingText.Substring(0, 1);
            remainingText = remainingText.Remove(0, 1);
            myText.text = "<color=" + myColorHex + ">" + selectedText + "</color>" + remainingText;
            lastCutTimeDelta -= timeNeededToPass;
        }
            
        // Change passed time since last Update
        lastCutTimeDelta += Time.deltaTime;
    }
}
