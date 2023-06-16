using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    // [SerializeField] SoundManager audioManager;
    [SerializeField] Toggle muteToggle;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TMP_Text bgmVolText;
    [SerializeField] TMP_Text sfxVolText;

    void OnEnable()
    {
        if (SoundManager.IsMuted())
        {
            muteToggle.SetIsOnWithoutNotify(true);
        }
        bgmSlider.value = SoundManager.BgmVolume;
        sfxSlider.value = SoundManager.SfxVolume;
        SetBgmVolText(bgmSlider.value);
        SetSfxVolText(sfxSlider.value);
    }

    public void UpdateUI()
    {
        if (SoundManager.IsMuted())
        {
            bgmSlider.SetValueWithoutNotify(0);
            SetBgmVolText(bgmSlider.value);
            bgmSlider.interactable = false;
            sfxSlider.SetValueWithoutNotify(0);
            SetSfxVolText(sfxSlider.value);
            sfxSlider.interactable = false;
        }
        else
        {
            bgmSlider.SetValueWithoutNotify(SoundManager.BgmVolume);
            sfxSlider.SetValueWithoutNotify(SoundManager.SfxVolume);
            SetBgmVolText(bgmSlider.value);
            bgmSlider.interactable = true;
            SetSfxVolText(sfxSlider.value);
            sfxSlider.interactable = true;
        }
    }

    public void ToggleMute()
    {
        SoundManager.Instance.ToggleMute();
    }

    public void SetBGMVol(float value)
    {
        SoundManager.Instance.SetBgmVolume(value);
    }

    public void SetSFXVol(float value)
    {
        SoundManager.Instance.SetSfxVolume(value);
    }

    public void SetBgmVolText(float value)
    {
        bgmVolText.text = Mathf.RoundToInt(bgmSlider.value * 100).ToString();
    }

    public void SetSfxVolText(float value)
    {
        sfxVolText.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
    }
}
