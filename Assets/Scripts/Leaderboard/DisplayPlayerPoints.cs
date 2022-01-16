using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class DisplayPlayerPoints : MonoBehaviourPunCallbacks
{
    public TMP_Text username;
    
    public Image avatar;
    public Sprite[] avatars;
    
    public TMP_Text pointsText;

    private int points;

    private void Start()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }

    public void SetUpPlayerPoints(Player player)
    {
        points = (int)player.CustomProperties["points"];
        pointsText.text = points.ToString();

        username.text = player.NickName;
        avatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
    }

    public int GetPlayerPoints()
    {
        return points;
    }
}
