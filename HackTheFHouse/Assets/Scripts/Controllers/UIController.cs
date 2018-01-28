using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public static UIController Instance { get { return instance; } }

    private Animator animator;
    [SerializeField]
    private Image fade;

	[SerializeField]
	private Slider infectionBar;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        animator = GetComponent<Animator>();
    }

    public void ButtonPlay()
    {
        GameController.instance.Play();
    }

    public void ButtonExit()
    {
        GameController.instance.Exit();
    }

    public void ButtonMainMenu()
    {
        GameController.instance.MainMenu();
    }

    public void ButtonCredits()
    {
        GameController.instance.Credits();
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

	public void EnableInfectionBar(float totalSeconds)
	{
		infectionBar.GetComponent<RectTransform> ().sizeDelta = new Vector2(100 + (20 * totalSeconds), 30);
		infectionBar.gameObject.SetActive (true);
	}

	public void UpdateInfectionBar(int percent)
	{
		infectionBar.value = (100 - percent);
	}

	public void DisableInfectionBar()
	{
		infectionBar.gameObject.SetActive (false);
	}

    public void GameOver (bool win)
    {
        if (win)
            Debug.Log("Win!");
        else
            Debug.Log("Loose!");
    }
}
