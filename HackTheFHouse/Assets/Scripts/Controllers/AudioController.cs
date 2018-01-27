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

    public void GameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }
}
