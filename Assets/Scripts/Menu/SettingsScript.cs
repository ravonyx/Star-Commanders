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


    public void SetVolume(float new_volume)
    {
        Debug.Log(VolumeSlider.value);
        AudioListener.volume = new_volume;
    }

    public void SetQuality(float new_quality)
    {
        Debug.Log(QualitySlider.value);

        QualitySettings.SetQualityLevel((int)new_quality, false);
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
