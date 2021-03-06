using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Photon.Pun;

public class TimerController : MonoBehaviour {
    [Header("UI fields")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Color almostDoneColor = new Color(233, 50, 50, 255);

    [Header("Gameplay values")]
    [SerializeField] private float totalSeconds = 120;
    [SerializeField] private UnityEvent OnStartFlash; 
    [SerializeField] private UnityEvent OnTimerEnd; 

    private float timeRemaining;
    private bool timerRunning = true;

    private AudioSource source;
    bool playTimerSoundOnce;

    void Start() {
        timeRemaining = totalSeconds;
        source = gameObject.GetComponent<AudioSource>();
        playTimerSoundOnce = false;
    }

    // Update is called once per frame
    void Update() {
        if (timerRunning) {
            if (timeRemaining < 14.2f && !playTimerSoundOnce)
            {
                source.PlayOneShot(source.clip);
                playTimerSoundOnce = true;
            } else if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime();
            } else { 
                timeRemaining = 0;
                timerRunning = false;
                StartCoroutine(FlashAndEndTimerCo());
            }
        }
    }

    public float GetRemainingTime()
    {
        return timeRemaining;
    }

    private void DisplayTime() {
        float minutes = Mathf.FloorToInt((timeRemaining + 1) / 60);
        float seconds = Mathf.FloorToInt((timeRemaining + 1) % 60);
        timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));

        if (timeRemaining <= 10)
            timerText.color = almostDoneColor;
    }
    IEnumerator FlashAndEndTimerCo() {
        if (OnStartFlash != null)
            OnStartFlash.Invoke();

        // Flash before transitioning 
        for (int i = 1; i <= 6; i++) {
            timerText.alpha = 0;
            yield return new WaitForSeconds(0.3f);
            timerText.alpha = 255;
            yield return new WaitForSeconds(0.3f);
        }

        OnTimerEnd.Invoke();
    }


}
