using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {
    [Header("UI Fields")]
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject creditsCanvas;

    public void OnClickCredits() {
        mainCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void OnClickBack() {
        mainCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

}
