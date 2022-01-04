using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerCursorItem : MonoBehaviourPunCallbacks
{
    public Image PlayerCursor;
    public Sprite[] cursors;

    // For debugging
    private Player player;
    public TMP_Text playerName;
    //

    private Canvas myCanvas;

    private void Start()
    {
        myCanvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
        
        if (PhotonNetwork.LocalPlayer.CustomProperties["cursorIndex"] != null)
        {
            PlayerCursor.sprite = cursors[(int)PhotonNetwork.LocalPlayer.CustomProperties["cursorIndex"]];
        } else
        {
            PlayerCursor.sprite = cursors[0];
        }

        playerName.text = PhotonNetwork.LocalPlayer.NickName + ": " + PhotonNetwork.LocalPlayer.CustomProperties["cursorIndex"];
    }

    // TODO
    public void ApplyLocalChanges()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //CreateUniqueCursor(player);

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }
}
