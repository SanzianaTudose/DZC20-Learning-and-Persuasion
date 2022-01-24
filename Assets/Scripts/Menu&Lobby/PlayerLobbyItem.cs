using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class PlayerLobbyItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    private AudioSource source;

    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image PlayerAvatar;
    public Sprite[] avatars;

    Player player;

    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        playerName.color = highlightColor;
        playerName.fontStyle = FontStyles.Bold;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);   
    }

    public void OnClickLeftArrow()
    {
        source.PlayOneShot(source.clip);

        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        } else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        source.PlayOneShot(source.clip);

        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] +  1;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player _player)
    {
        if (_player.CustomProperties.ContainsKey("playerAvatar"))
        {
            PlayerAvatar.sprite = avatars[(int)_player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)_player.CustomProperties["playerAvatar"];
        } else
        {
            PlayerAvatar.sprite = avatars[0];
            playerProperties["playerAvatar"] = 0;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }
}