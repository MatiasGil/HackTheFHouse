using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public static UIController Instance { get { return instance; } }

    private Animator animator;
    [SerializeField]
    private Image fade;

	[SerializeField]
	private GameObject startLevelBack;

    [SerializeField]
    public Button startLevel;

	[SerializeField]
	private GameObject winObject;
	[SerializeField]
	private GameObject looseObject;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += SceneLoaded;

        animator = GetComponent<Animator>();
    }
    
    public void ButtonPlay()
    {
        GameController.Instance.Play();
    }

    public void ButtonExit()
    {
        GameController.Instance.Exit();
    }

    public void ButtonMainMenu()
    {
        GameController.Instance.MainMenu();
    }

    public void ButtonCredits()
    {
        GameController.Instance.Credits();
    }

    public IEnumerator Fade(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                fade.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        else
        {
            fade.raycastTarget = true;
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fade.color = new Color(0, 0, 0, i);
                yield return null;
            }
            fade.raycastTarget = false;
        }
    }

    public void StartLevelButton()
    {
		startLevelBack.gameObject.SetActive (false);
        startLevel.gameObject.SetActive(false);
        GameController.Instance.StartLevel();
    }

    public void GameOver (bool win)
    {
		if (win)
			winObject.SetActive (true);
		else
			Invoke ("ShowLost", .1f);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tutorial")
        {
            startLevel.gameObject.SetActive(true);
			startLevelBack.gameObject.SetActive (true);
        }
    }

	public void ShowLost()
	{
		looseObject.SetActive (true);
	}

	public void BTN_PlayAgain()
	{
		SceneManager.LoadScene ("main");
		winObject.SetActive (false);
		looseObject.SetActive (false);
	}
}
