using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

/* Attached to CaseIntroCanvas. Called by SquadPicker.cs when timer ends. 
 * Handles picking a Case for the input squad and transitioning from Squad voting to Case introduction. 
 * input: picked Squad
 */
public class CaseIntroController : MonoBehaviour
{
    [Header("UI Fields")]
    [SerializeField] private GameObject squadVoteCanvas;
    [SerializeField] private TMP_Text squadText;
    [SerializeField] private TMP_Text caseText;
    [SerializeField] private GameObject continueButton;

    [Header("Gameplay Variables")]
    [SerializeField] private List<Case> allCases;

    private Dictionary<string, AudioClip> squadTooltips;

    public AudioClip inclusiveDesign;
    public AudioClip gamesAndPlay;
    public AudioClip vitality;
    public AudioClip transformingPractices;
    public AudioClip crafting;
    public AudioClip newFutures;
    public AudioClip health;
    public AudioClip sensoryMatters;
    public AudioClip artifice;

    public AudioSource audioSource;
    private bool playAudioOnce = false;

    public void startCaseIntro(string pickedSquad)
    {
        setUpDictionary();

        squadText.SetText(pickedSquad);

        // Transition UI
        squadVoteCanvas.SetActive(false);
        this.gameObject.SetActive(true);

        // Pick case for Squad
        string pickedCase = PickCaseforSquad(pickedSquad);
        caseText.SetText(pickedCase);

        // Only enable Continue button for MasterClient
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
            continueButton.SetActive(false);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
            SetSquadAndCaseRoomProperties(pickedSquad, pickedCase);

        startVoiceOver(pickedSquad);
    }

    private void startVoiceOver(string squadName)
    {
        if (!playAudioOnce)
        {
            if (squadTooltips.ContainsKey(squadName))
            {
                audioSource.PlayOneShot(squadTooltips[squadName]);
                playAudioOnce = true;
                continueButton.SetActive(false);
            }
        } else
        {
            if (!audioSource.isPlaying)
            {
                continueButton.SetActive(true);
            }
        }
    }

    private void setUpDictionary()
    {
        squadTooltips = new Dictionary<string, AudioClip>();
        squadTooltips.Add("Inclusive Design and Thoughtful Technology", inclusiveDesign);
        squadTooltips.Add("Games And Play", gamesAndPlay);
        squadTooltips.Add("Vitality", vitality);
        squadTooltips.Add("Transforming Practices", transformingPractices);
        squadTooltips.Add("Crafting Wearable Senses", crafting);
        squadTooltips.Add("New Futures (connectivity in the home)", newFutures);
        squadTooltips.Add("Health", health);
        squadTooltips.Add("Sensory Matters (sustainable food systems)", sensoryMatters);
        squadTooltips.Add("Artifice - Artificial Intelligence", artifice);
    }

    public void OnClickContinue()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }

    private string PickCaseforSquad(string pickedSquad)
    {
        List<Case> possibleCases = new List<Case>();

        foreach (Case it in allCases)
        {
            if (it.getSquadString() == pickedSquad)
                possibleCases.Add(it);
        }

        Case pickedCase = possibleCases[Random.Range(0, possibleCases.Count - 1)];
        return pickedCase.caseText;
    }
    private void SetSquadAndCaseRoomProperties(string pickedSquad, string pickedCase)
    {
        ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();
        roomProperties.Add("pickedSquad", pickedSquad);
        roomProperties.Add("pickedCase", pickedCase);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }
}
