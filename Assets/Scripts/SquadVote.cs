using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SquadVote : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    private VotesTracker votesTracker;
    public Image texture;
    private PhotonView view;



    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        string squadName = (string)instantiationData[0];

        votesTracker = FindObjectsOfType<VotesTracker>()[0];
        votesTracker.GetSquadObject(squadName).AddObjectAsMyChild(gameObject);

        view = GetComponent<PhotonView>();
        int avatarID = (int)view.Owner.CustomProperties["playerAvatar"];
        texture.sprite = votesTracker.GetAvatarImage(avatarID);
    }
}
