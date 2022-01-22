using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
    public Message messagePrefab;
    private Canvas canvas;

    private AudioSource source;

    public void DisplayNewMessage(string text)
    {
        source = gameObject.GetComponent<AudioSource>();
        source.PlayOneShot(source.clip);

        canvas = FindObjectOfType<Canvas>();
        Message newMessage = GameObject.Instantiate(messagePrefab, Vector3.zero, Quaternion.identity, canvas.transform);
        newMessage.SetMesssageText(text);
    }
}
