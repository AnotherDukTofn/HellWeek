using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessController : MonoBehaviour {
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Volume postProcessVolume;

    private ColorAdjustments colorAdjustments;

    private const float MinExposure = -2f;
    private const float MaxExposure = 2f;

    void Start() {
        if (postProcessVolume.profile.TryGet(out colorAdjustments)) {
            brightnessSlider.minValue = 0;
            brightnessSlider.maxValue = 100;

            float saved = PlayerPrefs.GetFloat("brightness", 75);
            brightnessSlider.value = saved;
            SetBrightness(saved);

            brightnessSlider.onValueChanged.AddListener(SetBrightness);
        }
    }

    private void SetBrightness(float value) {
        float t = value / 100f; 
        float exposure = Mathf.Lerp(MinExposure, MaxExposure, t);

        colorAdjustments.postExposure.value = exposure;
        PlayerPrefs.SetFloat("brightness", value);
    }
}