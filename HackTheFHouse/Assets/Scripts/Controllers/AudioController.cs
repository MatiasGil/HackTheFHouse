using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public static AudioController Instance { get { return instance; } }
    
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip mainMenuMusic;
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
        if (Input.GetKeyUp(KeyCode.J))
        {
            audioState = 2;
        }

        if (audioState == 2 && audioSource.volume > 0)
            audioSource.volume -= 1 * Time.deltaTime;

        if (audioState == 2 && audioSource.volume <= 0)
        {
            //GameMusic();
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

    public void GameMusic()
    {
        audioState = 2;
        audioSource.clip = gameMusic;
        audioSource.Play();
    }
}
