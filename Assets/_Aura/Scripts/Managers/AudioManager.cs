using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] UtilityController utilityController;
    [SerializeField] Image backGroundImage;
    [SerializeField] Sprite[] themeBackgrounds;
    [SerializeField] Slider volumeSlider;
    [SerializeField]AudioClip[] themeMusic;
    [SerializeField]AudioClip correctAnswerFX;
    [SerializeField] AudioClip wrongAnswerFX;
    [SerializeField] AudioClip buttonPressFX;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
                if(instance == null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        PlayChosenTheme(16);
        volumeSlider.value = audioSource.volume;
    }

    public void PlayChosenTheme(int index)
    {
        backGroundImage.sprite = themeBackgrounds[index];
        audioSource.PlayOneShot(buttonPressFX);
        audioSource.clip = themeMusic[index];
        audioSource.Play();
        audioSource.loop = true;
    }

    public void PlayCorrectAnswerFX()
    {
        audioSource.PlayOneShot(correctAnswerFX);
    }

    public void PlayWrongAnswerFX()
    {
        audioSource.PlayOneShot(wrongAnswerFX);
    }

    public void SetAudioVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ReturnToGame()
    {

        utilityController.HideAudioMenu();
    }
}
