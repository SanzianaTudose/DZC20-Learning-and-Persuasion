using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoScrollingText : MonoBehaviour
{
    public TMP_Text myText;
    public float speed = 15;
    private Vector3 textPosition;

    void Start()
    {
        textPosition = myText.transform.position;
    }

    void Update()
    {
        if (myText.transform.localPosition.y < 1500f)
        {
            textPosition = new Vector3(textPosition.x, textPosition.y + 0.0001f * speed, textPosition.z);
            myText.transform.position = textPosition;
        }
    }
}
