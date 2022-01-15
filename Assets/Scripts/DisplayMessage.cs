using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public Message messagePrefab;
    private Canvas canvas;

    public void DisplayNewMessage(string text)
    {
        canvas = FindObjectOfType<Canvas>();
        Message newMessage = GameObject.Instantiate(messagePrefab, Vector3.zero, Quaternion.identity, canvas.transform);
        newMessage.SetMesssageText(text);
    }
}
