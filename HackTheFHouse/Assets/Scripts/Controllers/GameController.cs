using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour 
{
    public static GameController instance;
    public static GameController Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

	public void Play ()
	{
        StartCoroutine(UIController.instance.Fade(false));
        SceneManager.LoadScene("Home");
        StartCoroutine(UIController.instance.Fade(true));
        AudioController.instance.GameMusicIntro();
    }

    public void Exit()
    {
        StartCoroutine(UIController.instance.Fade(false));
        Application.Quit();
    }

    public void MainMenu ()
    {
        StartCoroutine(UIController.instance.Fade(false));
        SceneManager.LoadScene("Main");
        StartCoroutine(UIController.instance.Fade(true));
    }

    public void Credits()
    {
        StartCoroutine(UIController.instance.Fade(false));
        SceneManager.LoadScene("Credits");
        StartCoroutine(UIController.instance.Fade(true));
    }

}
