using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class GameController : MonoBehaviour 
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }

	public bool readyToNextLevel { get; private set;}

	private Dictionary<string, ElectricElement> notInfectedElectricElements = new Dictionary<string, ElectricElement>();
	private Dictionary<string, ElectricElement> infectedElectricElements = new Dictionary<string, ElectricElement>();

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
        SceneManager.LoadScene("Tutorial");
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

	public void AddElectricElementToDB(ElectricElement electricElement)
	{
		notInfectedElectricElements.Add (electricElement.name, electricElement);
	}

	public void ElectricElementInfected(ElectricElement electricElement)
	{
		if (notInfectedElectricElements.ContainsKey (electricElement.name)) 
		{
			
			notInfectedElectricElements.Remove (electricElement.name);
			infectedElectricElements.Add (electricElement.name, electricElement);
		}

		if (notInfectedElectricElements.Count == 1) 
		{
			readyToNextLevel = true;
			notInfectedElectricElements.ElementAt (0).Value.ReadyToBeInfected ();
		}
	}

	public void StartLevel()
	{
		PlayerController.Instance.StartGame ();
	}

	public void LevelFinished()
	{
		notInfectedElectricElements.Clear ();
		infectedElectricElements.Clear ();
		readyToNextLevel = false;
	}
}
