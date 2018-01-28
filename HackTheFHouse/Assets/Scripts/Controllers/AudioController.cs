using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public static AudioController Instance { get { return instance; } }
    
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource audioLoop;

	[SerializeField]
	private GameObject audioSourceSFXPrefab;

    [SerializeField]
    private AudioClip mainMenuMusic;
    [SerializeField]
    private AudioClip gameMusicIntro;
    [SerializeField]
    private AudioClip gameMusic;

	[SerializeField]
	private List<AudioClip> audioClipsSFX;

	private Dictionary<string, AudioClip> audioSFXMap = new Dictionary<string, AudioClip>();

    private int audioState = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();

		foreach (AudioClip clip in audioClipsSFX) {
			audioSFXMap.Add (clip.name, clip);
		}
    }

    private void Start()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.Play();
    }

    private void Update()
    {
        if (audioState == 2 && audioSource.volume > 0)
            audioSource.volume -= 1 * Time.deltaTime;

        if (audioState == 2 && audioSource.volume <= 0)
        {
            audioState = 3;
        }

        if (audioState == 3 && audioSource.volume <= 1)
            audioSource.volume += 1 * Time.deltaTime;

        if (audioState == 3 && audioSource.volume >= 1)
        {
            audioSource.volume = 1;
            audioState = 1;
        }
    }

    public void GameMusicIntro()
    {
        //audioState = 2;
        audioSource.clip = gameMusicIntro;
        audioSource.Play();
        StartCoroutine(IntroAndContinue());
    }

	public void PlaySFX(string name, bool changeTimer = false, float Timer = 1)
	{
		Debug.Log (name);
		if (audioSFXMap.ContainsKey (name)) {
			StartCoroutine (PlaySFXCoroutine (audioSFXMap[name], changeTimer, Timer));
		}
	}

	public void StopSFX()
	{
		
	}

	private IEnumerator PlaySFXCoroutine(AudioClip audioClip, bool changeTimer = false, float Timer = 1)
	{
		GameObject audioSourceObject = Instantiate(audioSourceSFXPrefab) as GameObject;
		audioSourceObject.transform.SetParent (this.transform);
		AudioSource thisAudioSource = audioSourceObject.GetComponent<AudioSource> ();
		thisAudioSource.clip = audioClip;
		thisAudioSource.Play ();

		yield return new WaitForSeconds (audioClip.length);

		thisAudioSource.Stop ();
		Destroy (audioSourceObject);
	}

    private IEnumerator IntroAndContinue()
    {
        yield return new WaitForSeconds(4.65f);
        GameMusicLoop();
    }

    private void GameMusicLoop()
    {
        audioLoop.Play();
    }
}
