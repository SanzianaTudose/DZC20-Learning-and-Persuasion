using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ShowGrandma : MonoBehaviour
{
    public GameObject grandmaObject;

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("pickedSquad"))
        {
            string pickedSquad = (string)PhotonNetwork.CurrentRoom.CustomProperties["pickedSquad"];
            if (pickedSquad.Equals("Health"))
            {
                grandmaObject.SetActive(true);
                return;
            }
        } 
        
        grandmaObject.SetActive(false);
    }
}
