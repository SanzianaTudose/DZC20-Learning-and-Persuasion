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
    private GameObject playerCursor;

    void Start()
    {
        playerCursor = (GameObject)PhotonNetwork.Instantiate(playerCursorPrefab.name, Vector3.zero, Quaternion.identity);
        playerCursor.transform.SetParent(playerPrefabParent.transform);
        playerCursor.transform.localScale = Vector3.one;
    }
}
