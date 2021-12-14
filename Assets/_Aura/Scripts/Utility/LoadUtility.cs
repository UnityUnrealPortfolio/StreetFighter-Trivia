using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadUtility : MonoBehaviour
{
    [SerializeField] Image loadProgressBar;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip loadGameFX;
    [SerializeField] AudioClip exitGameFX;
    bool loadingGame = false;
    public void LoadGame()
    {
        audioSource.PlayOneShot(loadGameFX);
        loadingGame = true;
        SceneManager.LoadSceneAsync(1);
        StartCoroutine(loadNextScene());
    }
    public void ExitGame()
    {
        audioSource.PlayOneShot(exitGameFX);
        Application.Quit();
    }

    IEnumerator loadNextScene()
    {   
        yield return new WaitForSeconds(.1f);
        while(SceneManager.GetActiveScene().buildIndex == 0)
        { 
            yield return new WaitForSeconds(.1f);
            loadProgressBar.fillAmount += Time.deltaTime/20f;
        }

    }

    private void Awake()
    {
        loadingGame = false;
    }
    private void Update()
    {
        
    }
}
