using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityController : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] GameObject tryAgainCanvas;
    [SerializeField] GameObject audioPage;
    bool audioMenuShowing;
    public void ExitApplication()
    {
        Application.Quit();
    }

    private void Awake()
    {
        audioPage.SetActive(false);
        audioMenuShowing = false;
    }

    public void ShowAudioMenu()
    {
        tryAgainCanvas.SetActive(false);
        GameManager.Instance.RunTimer = false;
        TogglePages(false);
        audioPage.SetActive(true);
        audioMenuShowing = true;

    }

    public void TogglePages(bool toggle)
    {
        foreach (var page in pages)
        {
            page.SetActive(toggle);
        }

    }

    public void HideAudioMenu()
    {
        GameManager.Instance.OnTryAgainClickHandler();
        TogglePages(true);
        audioPage.SetActive(false);
        audioMenuShowing = false;
    }
}
