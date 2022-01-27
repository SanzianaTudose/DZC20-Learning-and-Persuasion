using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SkipTimer : MonoBehaviour
{
    public TimerController timer;
    public Button buttonComponent;

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            gameObject.SetActive(true);
            if (timer.GetRemainingTime() < 15f)
            {
                buttonComponent.interactable = false;
            } else
            {
                buttonComponent.interactable = true;
            }
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
