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
    public Sprite transparent;

    private Canvas myCanvas;
    PhotonView view;

    private void Start()
    {
        // get the canvas object and viewComponent of current object
        myCanvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
        view = GetComponent<PhotonView>();

        // Set how the cursor should look 
        SetCursorAppearance();

        // Apply local changes to only my cursor
        ApplyLocalChanges();
    }

    public void SetCursorAppearance()
    {   
        // Set image of cursor
        if (view.Owner.CustomProperties["cursorIndex"] != null)
        {
            PlayerCursor.sprite = cursors[(int)view.Owner.CustomProperties["cursorIndex"]];
        }
        else
        {
            PlayerCursor.sprite = cursors[3];
        }

        // Set cursor object parent and scale
        transform.SetParent(myCanvas.transform);
        transform.localScale = Vector3.one;
    }

    public void ApplyLocalChanges()
    {
        if (view.IsMine)
        {
            //playerName.text = "MINE";
            PlayerCursor.sprite = transparent;
        }
    }

    private void Update()
    {
        // Update this player's cursor only if the mouse is over the game window
        if (view.IsMine && IsMouseOverGameWindow)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
        }
    }

    bool IsMouseOverGameWindow { get { return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y); } }
}
