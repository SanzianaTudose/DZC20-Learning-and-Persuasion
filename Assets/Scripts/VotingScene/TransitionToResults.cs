using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToResults : MonoBehaviour
{
    public AnswerDisplayController answersContainer;
    private List<Answer> answers;

    private bool coroutineAllowed;

    void Start()
    {
        coroutineAllowed = true;
        answers = new List<Answer>();
    }

    public void StartRotatingAnswers()
    {
        foreach (Answer ans in answersContainer.answerObjs)
        {
            if (ans.gameObject.activeInHierarchy) answers.Add(ans);
        }

        //RotateAnswer(answer);
        //if (coroutineAllowed)
        //{
        //    StartCoroutine(RotateAnswer(answer));
        //}

        RotateNextAnswer();
    }

    public void RotateNextAnswer()
    {
        if (answers.Count == 0) return;

        StartCoroutine(RotateAnswer(answers[0]));
    }

    private IEnumerator RotateAnswer(Answer currentAnswer)
    {
        coroutineAllowed = false;
        int score = currentAnswer.pointsThisRound;

        for (float i = 0f; i <= 180f; i += 10f)
        {
            currentAnswer.transform.rotation = Quaternion.Euler(0f, i, 0f);

            //if (i == 90f) currentAnswer.answerText.text = score.ToString();
            if (i == 90f) currentAnswer.answerText.text = "100";
            if (i >= 90f) currentAnswer.answerText.transform.rotation = Quaternion.Euler(0f, -1f * i, 0f);

            yield return new WaitForSeconds(0.05f);
        }

        coroutineAllowed = true;

        answers.Remove(answers[0]);
        RotateNextAnswer();
    }
}


//public void StartRotatingAnswers()
//{
//    Debug.Log("StartRotatingAnswers");

//    //Remove all votes
//        foreach (Answer answer in answers)
//    {
//        answer.RemovePreviousVotes();
//    }

//    //Rotate each card
//        RotateAnswer(answers[0]);
//    foreach (Answer answer in answers)
//    {
//        RotateAnswer(answer);
//    }
//}


