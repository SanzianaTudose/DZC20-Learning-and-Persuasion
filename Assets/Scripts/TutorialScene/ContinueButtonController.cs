using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonController : MonoBehaviour {
    [SerializeField] private AudioSource profAudioSource;
    [SerializeField] private GameObject idCanvas;
    [SerializeField] private GameObject gameCanvas;
    private Button continueButton;

    public AudioSource backgroundMusic;

    void Start() {
        continueButton = GetComponent<Button>();
        continueButton.interactable = false;

        idCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    void Update() {
        // Enable Continue button when audio is finished
        if (!profAudioSource.isPlaying) {
            continueButton.interactable = true;
        }
    }

    public void OnClickContinue() {
        // TODO: maybe add a nice transition
        backgroundMusic.PlayOneShot(backgroundMusic.clip);
        gameCanvas.SetActive(true);
        idCanvas.SetActive(false);
    }
}
