using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoScrollingText : MonoBehaviour
{
    // Text object, speed and position
    public TMP_Text myText;
    public float speed;
    private Vector3 textPosition;

    // Text selection color and variable to store it as HEX value
    public Color textSelectionColor;
    private string myColorHex;

    // Variables to keep track of which text is selected
    private string remainingText;
    private string selectedText;

    // Keep track of time passed since last update of text selection
    private float lastCutTimeDelta;


    void Start()
    {
        // Set variables
        textPosition = myText.transform.position;
        remainingText = myText.text;
        selectedText = "";
        myColorHex = "#" + ColorUtility.ToHtmlStringRGB(textSelectionColor);
    }

    void Update()
    {
        // If text object is at the end stop moving it
        if (myText.transform.localPosition.y < 1500f)
        {
            // Set new position of text object
            textPosition = new Vector3(textPosition.x, textPosition.y + 0.0001f * speed, textPosition.z);
            myText.transform.position = textPosition;

        }

        // If enough time has passed update selection text
        if (lastCutTimeDelta > 0.0525 && remainingText.Length > 1)
        {
            // Update selection text
            selectedText += remainingText.Substring(0, 1);
            remainingText = remainingText.Remove(0, 1);
            myText.text = "<color=" + myColorHex + ">" + selectedText + "</color>" + remainingText;
            lastCutTimeDelta = 0;
        }
            
        // Change passed time since last Update
        lastCutTimeDelta += Time.deltaTime;
    }
}