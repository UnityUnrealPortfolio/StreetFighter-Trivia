using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuizQuestionSO quizQuestionSO;
    [SerializeField] TextMeshProUGUI questionTextMesh;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite correctAnswerBtnSprite;
    [SerializeField] Sprite defaultAnswerBtnSprite;
    int correctAnswerIndex = 0;
    [SerializeField]int currentQuizObjectIndex = 0;
    QuizQuestionSO currentQuizQuestionSO;
    

    public void SetUpQuizPage(QuizQuestionSO quizQuestion)
    {
        currentQuizQuestionSO = quizQuestion;
        questionTextMesh.text = currentQuizQuestionSO.GetQuestionText();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            var btnTextMesh = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            btnTextMesh.text = currentQuizQuestionSO.GetAnswerText(i);
            answerButtons[i].GetComponent<Button>().image.sprite = defaultAnswerBtnSprite;
        }
    }

    public void OnAnswerButtonClicked(int index)
    {
        correctAnswerIndex = currentQuizQuestionSO.GetCorrectAnswerIndex();
        
        if (index == currentQuizQuestionSO.GetCorrectAnswerIndex())
        {
            questionTextMesh.text = "Correct!";
            answerButtons[index].GetComponent<Button>().image.sprite = correctAnswerBtnSprite;
            GameManager.Instance.GotAnswerCorrect = true;
        }
        else
        {
            questionTextMesh.text ="Wrong! The correct anser is\n"+
                string.Format(" '{0}' ", currentQuizQuestionSO.GetAnswerText(correctAnswerIndex));

            answerButtons[correctAnswerIndex].GetComponent<Button>().image.sprite = correctAnswerBtnSprite;
            GameManager.Instance.GotAnswerCorrect = false;
        }
       

    }

    public void SetAnswerButtonsState(bool state)
    {
        for(int i = 0;i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }
}
