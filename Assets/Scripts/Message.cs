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
        transform.localPosition = Vector3.zero;
    }

    public void CloseMessage()
    {
        StartCoroutine(WaitForClosing());
    }

    private IEnumerator WaitForClosing()
    {
        yield return new WaitForSeconds(0.05f);
        GameObject.Destroy(this.gameObject);
    }

    public void SetMesssageText(string text)
    {
        messageText.text = text;
    }
}