using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [SerializeField] GameObject tryAgainMenu;
    [SerializeField] TimerUIController timerController;
    [SerializeField]List<QuizQuestionSO> quizQuestionSOs = new List<QuizQuestionSO>();
    [SerializeField] Quiz quizPage;
    [SerializeField]int currentQuizQuestion = 0;
    public int CurrentQuizQuestion
    {
        get => currentQuizQuestion;
        set
        {
          
            currentQuizQuestion = value;
            if(currentQuizQuestion == totalQuestionsInQuiz)
            {
                currentQuizQuestion = 0;
            }
        }
    }
    int totalQuestionsInQuiz = 4;

    bool gotAnswerCorrect;
    public bool GotAnswerCorrect
    {
        set
        {
            gotAnswerCorrect = value;

            if(currentQuizQuestion == totalQuestionsInQuiz)
            {
                currentQuizQuestion = 0;
                ProgressToNext();
            }
            if (gotAnswerCorrect && currentQuizQuestion < totalQuestionsInQuiz)
            {
                //progress to next question
                runTimer = false;
                ProgressToNext();
                CurrentQuizQuestion++;
            }
            
            else if(gotAnswerCorrect == false)
            {
                //show try again menu
                OnTryAgainHandler();
                runTimer = false;

            }
          
        }
    }

    #region Singleton Setup
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>().GetComponent<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        tryAgainMenu.SetActive(false);
        totalQuestionsInQuiz = quizQuestionSOs.Count;
        currentQuizQuestion = 0;
        quizPage.SetUpQuizPage(quizQuestionSOs[currentQuizQuestion]);
        quizPage.SetAnswerButtonsState(true);
        ResetTimer();

    }
    private void Update()
    {
        if (runTimer)
        {
            CurrentTimer -= Time.deltaTime;
        }
    }
    #endregion

    #region Timer Utility
    [SerializeField]float timeToAnswer = 5f;
    public float GetTimeToAnswer()
    {
        return timeToAnswer;
    }
    [SerializeField] float currentTimer;
    private float CurrentTimer
    {
        get => currentTimer;
        set
        {
            currentTimer = value;
            timerController.SetTimerFill(currentTimer);
            if (currentTimer < 0)
            {
                //run time over event
                OnTryAgainHandler();
                runTimer = false;
            }
        }
    }
    bool timeToAnswerElapsed;
    bool runTimer;
    public bool RunTimer
    {
        set { runTimer = value; }
    }
    public void ResetTimer()
    {
        CurrentTimer = timeToAnswer;
        runTimer = true;
    } 
    #endregion

    #region Progress Utility
    private void ProgressToNext()
    {
        StartCoroutine(OntoNextQuestion());
    }

    IEnumerator OntoNextQuestion()
    {
        quizPage.SetAnswerButtonsState(false);
        yield return new WaitForSeconds(2f);
        ResetTimer();
        quizPage.SetUpQuizPage(quizQuestionSOs[currentQuizQuestion]);
        quizPage.SetAnswerButtonsState(true);
    }
    private void OnTryAgainHandler()
    {
        //disable buttons
        quizPage.SetAnswerButtonsState(false);
        //show try again menu
        tryAgainMenu?.SetActive(true);
    }

    public void OnTryAgainClickHandler()
    {
        //reset timer
        ResetTimer();
        //Reload previous Question
        quizPage.SetUpQuizPage(quizQuestionSOs[currentQuizQuestion]);
        quizPage.SetAnswerButtonsState(true);
        tryAgainMenu.SetActive(false);
    }
    #endregion


}
