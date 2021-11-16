using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsItem : MonoBehaviour
{
    [SerializeField]
    private string PlayerPrefName;

    [SerializeField]
    private Component component;

    [SerializeField]
    private SettingsType type;

    void Start() =>
        LoadValue();

    public void LoadValue()
    {
        switch (type)
        {
            case SettingsType.Slider:
                Slider slider = (Slider)component;
                slider.value = PlayerPrefs.GetFloat(PlayerPrefName, slider.value);
                break;
            case SettingsType.Toggle:
                Toggle toggle = (Toggle)component;
                toggle.isOn = PlayerPrefs.GetInt(PlayerPrefName, toggle.isOn ? 1 : 0) >= 1;
                break;
            default:
                break;
        }
    }

    public void SetPlayerPref(float value)
    {
        PlayerPrefs.SetFloat(PlayerPrefName, value);
    }

    public void SetPlayerPref(bool value)
    {
        PlayerPrefs.SetInt(PlayerPrefName, value ? 1 : 0);
    }

    enum SettingsType
    {
        Slider,
        Toggle
    }
}
