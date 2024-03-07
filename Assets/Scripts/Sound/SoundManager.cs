using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float footstepPlayOffset = 0.5f;
    [SerializeField, Range(0, 1)] private float globalVolume;
    [SerializeField, Range(0, 1)] private float footstepsVolume;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioClip clickSound;

    public event Action OnMuteSoundChange;
    public event Action OnMuteMusicChange;

    private AudioSource audioSource;
    private float timer;
    private bool isMutedSound = false;
    private bool isMutedMusic = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        timer = footstepPlayOffset;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        isMutedSound = !GameManager.instance.GetWallet().isSoundOn;
        isMutedMusic = !GameManager.instance.GetWallet().isMusicOn;
        Debug.Log(GameManager.instance.GetWallet().isSoundOn);
        MuteMusic();
    }
    public void PlayFootsteps()
    {
        if (!isMutedSound)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                AudioClip randomFootstepClip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
                audioSource.PlayOneShot(randomFootstepClip, footstepsVolume);
                timer = footstepPlayOffset;
            }
        }
    }
    public void PlaySound(AudioClip audioClip)
    {
        if (!isMutedSound)
        {
            audioSource.PlayOneShot(audioClip, globalVolume);
        }
    }

    public void PlayClickSound()
    {
        if (!isMutedSound)
        {
            audioSource.PlayOneShot(clickSound, globalVolume);
        }
    }

    public void ToggleMuteSounds()
    {
        PlayClickSound();
        GameManager.instance.GetWallet().ToggleSound();
        isMutedSound = !isMutedSound;
        OnMuteSoundChange?.Invoke();
    }
    public void ToggleMuteMusic()
    {
        PlayClickSound();
        GameManager.instance.GetWallet().ToggleMusic();
        isMutedMusic = !isMutedMusic;
        OnMuteMusicChange?.Invoke();
        MuteMusic();
    }

    private void MuteMusic()
    {
        if (isMutedMusic)
        {
            musicAudioSource.Stop();
        }
        else
        {
            musicAudioSource.Play();
        }
    }
}
