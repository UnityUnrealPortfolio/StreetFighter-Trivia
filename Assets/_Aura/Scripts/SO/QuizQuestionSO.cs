using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Question", fileName = "NewQuestion")]
public class QuizQuestionSO : ScriptableObject
{
    [TextArea(2, 7)]
    [SerializeField]
    string questionText = "New Question text goes here";

    [SerializeField]
    string[] answerTexts;

    [Range(0f, 3f)]
    [SerializeField]
    int correctAnswerIndex = 0;

    public string QuestionText
    {
        get { return questionText; }
    }

    public string GetQuestionText()
    {
        return questionText;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswerText(int index)
    {
        return answerTexts[index];
    }
}
