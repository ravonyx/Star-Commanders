using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    private Dropdown qualityDropdown;
    [SerializeField]
    private Dropdown screenDropdown;
    [SerializeField]
    Slider VolumeSlider;
    [SerializeField]
    Toggle AntiAlias;

    void Start()
    {
        for(int i = 0; i < Screen.resolutions.Length; i++)
            screenDropdown.options.Add(new Dropdown.OptionData() { text = Screen.resolutions[i].width + " X " + Screen.resolutions[i].height});
        screenDropdown.GetComponentInChildren<Text>().text = Screen.width + " X " + Screen.height;

        for (int i = 0; i < QualitySettings.names.Length; i++)
            qualityDropdown.options.Add(new Dropdown.OptionData() { text = QualitySettings.names[i] });
        qualityDropdown.GetComponentInChildren<Text>().text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }

    public void SetResolution()
    {
        Screen.SetResolution(Screen.resolutions[screenDropdown.value].width, Screen.resolutions[screenDropdown.value].height, Screen.fullScreen);
    }

    public void SetVolume()
    {
        AudioListener.volume = VolumeSlider.value;
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel((int)qualityDropdown.value, true);
    }

    public void SetAntiAlias(bool activated)
    {
        Debug.Log(AntiAlias.isOn);

        if (activated)
        {
            QualitySettings.antiAliasing = 2;
        }
        else
        {
            QualitySettings.antiAliasing = 0;
        }
    }
}
