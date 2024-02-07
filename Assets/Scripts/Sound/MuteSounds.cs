using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSounds : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Image muteSoundButtonImage;
    [SerializeField] private Image muteMusicButtonImage;

    private bool isMutedSound = false;
    private bool isMutedMusic = false;

    private void Start()
    {
        SoundManager.Instance.OnMuteMusicChange += SoundManager_OnMuteMusicChange;
        SoundManager.Instance.OnMuteSoundChange += SoundManager_OnMuteSoundChange;
    }

    private void SoundManager_OnMuteSoundChange()
    {
        MuteSound();
    }

    private void SoundManager_OnMuteMusicChange()
    {
        MuteMusic();
    }

    public void MuteSound()
    {
        isMutedSound = !isMutedSound;
        UpdateMuteSoundButtonImage();
    }
    private void UpdateMuteSoundButtonImage()
    {
        if (isMutedSound)
        {
            muteSoundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            muteSoundButtonImage.sprite = soundOnSprite;
        }
    }

    public void MuteMusic()
    {
        isMutedMusic = !isMutedMusic;
        UpdateMuteMusicButtonImage();
    }
    private void UpdateMuteMusicButtonImage()
    {
        if (isMutedMusic)
        {
            muteMusicButtonImage.sprite = musicOffSprite;
        }
        else
        {
            muteMusicButtonImage.sprite = musicOnSprite;
        }
    }
}
