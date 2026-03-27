using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Na razie KOMENTUJEMY – ¿eby nie wywala³o b³êdu
        // if (AudioMenager.Instance != null)
        //     AudioMenager.Instance.SetVolume(savedVolume);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;

        // Na razie KOMENTUJEMY – ¿eby nie wywala³o b³êdu
        // if (AudioMenager.Instance != null)
        //     AudioMenager.Instance.SetVolume(value);

        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }
}