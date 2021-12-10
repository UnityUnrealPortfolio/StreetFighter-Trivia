using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIController : MonoBehaviour
{
    [SerializeField] GameObject timerImage;
    [SerializeField] float fillAmnt;
    public void SetTimerFill(float fillAmount)
    {
        fillAmnt = fillAmount / GameManager.Instance.GetTimeToAnswer();
    }

    private void Update()
    {
        timerImage.GetComponent<Image>().fillAmount = fillAmnt;
    }
}


