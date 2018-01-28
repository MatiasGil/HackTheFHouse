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
    private AudioClip mainMenuMusic;
    [SerializeField]
    private AudioClip gameMusicIntro;
    [SerializeField]
    private AudioClip gameMusic;

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

        //if (audioSource.clip == gameMusicIntro && !audioSource.isPlaying)
            //GameMusicLoop();
    }

    public void GameMusicIntro()
    {
        //audioState = 2;
        audioSource.clip = gameMusicIntro;
        audioSource.Play();
        StartCoroutine(IntroAndContinue());
        //IntroAndContinue();
    }

    private IEnumerator IntroAndContinue()
    {
        //audioSource.Play();
        //audioLoop.Play();
        //audioLoop.Pause();
        
        yield return new WaitForSeconds(4.65f);
    
        //audioSource.Stop();
        GameMusicLoop();
    }

    private void GameMusicLoop()
    {
        audioLoop.Play();
    }
}
