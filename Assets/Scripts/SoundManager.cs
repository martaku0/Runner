using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private bool muted;
    public AudioSource audioSource;

    public AudioClip click;
    public AudioClip walking;
    public AudioClip jump;

    public AudioSource effectsSource;
    public AudioSource menuSoundSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMuted()
    {
        muted = !muted;

        audioSource.mute = muted;
    }

    public bool GetMuted()
    {
        return muted;
    }

    public void PlayOnceClick()
    {
        if (muted)
        {
            return;
        }
           
        effectsSource.PlayOneShot(click, 1f);
    }

    public void PlayOnceJump()
    {
        if (muted)
        {
            return;
        }

        effectsSource.PlayOneShot(jump, 1f);
    }

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
