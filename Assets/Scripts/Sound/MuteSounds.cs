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


    private void Start()
    {
        SoundManager.Instance.OnMuteMusicChange += SoundManager_OnMuteMusicChange;
        SoundManager.Instance.OnMuteSoundChange += SoundManager_OnMuteSoundChange;
        MuteSound();
        MuteMusic();
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
        UpdateMuteSoundButtonImage();
    }
    private void UpdateMuteSoundButtonImage()
    {
        if (!GameManager.instance.GetWallet().isSoundOn)
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
        UpdateMuteMusicButtonImage();
    }
    private void UpdateMuteMusicButtonImage()
    {
        if (!GameManager.instance.GetWallet().isMusicOn)
        {
            muteMusicButtonImage.sprite = musicOffSprite;
        }
        else
        {
            muteMusicButtonImage.sprite = musicOnSprite;
        }
    }
}
