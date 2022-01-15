using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Message : MonoBehaviour
{
    public TMP_Text messageText;

    private void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void CloseMessage()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void SetMesssageText(string text)
    {
        messageText.text = text;
    }
}