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
        // Get the squad that this vote is for
        object[] instantiationData = info.photonView.InstantiationData;
        string squadName = (string)instantiationData[0];

        // Get vote tracker used to connect the vote with the squad
        votesTracker = FindObjectsOfType<VotesTracker>()[0];

        // Add vote to the correct squad and format it
        votesTracker.GetSquadObject(squadName).AddObjectAsMyChild(gameObject);

        // Add correct texture to the vote corresponding to the avatar of the person voting
        view = GetComponent<PhotonView>();
        int avatarID = (int)view.Owner.CustomProperties["playerAvatar"];
        texture.sprite = votesTracker.GetAvatarImage(avatarID);
    }
}
