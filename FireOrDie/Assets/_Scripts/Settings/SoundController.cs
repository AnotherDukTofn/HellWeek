using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour {
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeSlider;

    void Start() {
        SetVolume();
    }

    public void SetVolume() {
        float volume = volumeSlider.value;
        mixer.SetFloat("MasterVolume", volume - 80);  
    }
}