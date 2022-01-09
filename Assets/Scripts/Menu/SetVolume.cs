using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    private float volume;
    private Slider slider;

    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        volume = GlobalData.MainMenuSliderValue;
        slider.value = volume;
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void setValue(float sliderVal)
    {
        GlobalData.MainMenuSliderValue = sliderVal;
        mixer.SetFloat("MasterVolume", Mathf.Log10(GlobalData.MainMenuSliderValue) * 20);
    }
}
