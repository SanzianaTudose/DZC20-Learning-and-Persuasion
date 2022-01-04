using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCursorSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerCursorPrefab;
    public GameObject playerPrefabParent;
    //private GameObject playerCursor;
    private GameObject[] playerCursors;
    private int lastCursorsAmount;

    void Start()
    {
        PhotonNetwork.Instantiate(playerCursorPrefab.name, Vector3.zero, Quaternion.identity);

        lastCursorsAmount = -1;
        SetCursorPositions();
    }
    void SetCursorPositions ()
    {
        playerCursors = GameObject.FindGameObjectsWithTag("PlayerCursor");

        if (playerCursors.Length == lastCursorsAmount) return;

        foreach (GameObject cursor in playerCursors)
        {
            cursor.transform.SetParent(playerPrefabParent.transform);
            cursor.transform.localScale = Vector3.one;
        }

        lastCursorsAmount = playerCursors.Length;
    }

    private void Update()
    {
        SetCursorPositions();

        //Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);
        //Debug.Log(playerCursors.Length);
    }
}
