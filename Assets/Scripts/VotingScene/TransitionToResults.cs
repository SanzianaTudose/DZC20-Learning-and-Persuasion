using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;


public class TransitionToResults : MonoBehaviourPunCallbacks
{
    public AnswerDisplayController answersContainer;
    private List<Answer> answers;
    private AudioSource source;

    void Start()
    {
        answers = new List<Answer>();
        source = gameObject.GetComponent<AudioSource>();
    }

    public void StartRotatingAnswers()
    {
        foreach (Answer ans in answersContainer.answerObjs)
        {
            if (ans.gameObject.activeInHierarchy) answers.Add(ans);
        }

        RotateNextAnswer();
    }

    public void RotateNextAnswer()
    {
        if (answers.Count == 0)
        {
            GoToLeaderboard();
            return;
        }
        StartCoroutine(RotateAnswer(answers[0]));
    }

    private IEnumerator RotateAnswer(Answer currentAnswer)
    {
        source.PlayOneShot(source.clip);

        int score = currentAnswer.pointsThisRound;

        for (float i = 0f; i <= 180f; i += 10f)
        {
            currentAnswer.transform.rotation = Quaternion.Euler(0f, i, 0f);

            if (i == 90f) currentAnswer.ShowBack(score);
            if (i > 90f) currentAnswer.backSide.transform.rotation = Quaternion.Euler(0f, -1f*(180-i), 0f);

            yield return new WaitForSeconds(0.05f);
        }

        answers.Remove(answers[0]);
        RotateNextAnswer();
    }

    private void GoToLeaderboard()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Leaderboard");
        }
    }
}



