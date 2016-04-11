using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    Slider VolumeSlider;
    [SerializeField]
    Slider QualitySlider;
    [SerializeField]
    Toggle AntiAlias;

    void Start()
    {
        VolumeSlider.value = AudioListener.volume;
        QualitySlider.value = QualitySettings.GetQualityLevel();
        AntiAlias.isOn = QualitySettings.antiAliasing > 0 ? true : false;
    }

    public void SetVolume(float new_volume)
    {
        AudioListener.volume = new_volume;
    }

    public void SetQuality(float new_quality)
    {
        QualitySettings.SetQualityLevel((int)new_quality, false);
    }

    public void SetAntiAlias(bool activated)
    {
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
