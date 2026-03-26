using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // zmienne do podmianki paneli
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    // zmienne do obslugi suwaka glosnosci i jego tekstu
    public TMP_Text volumeNumber;
    public Slider volumeSlider;

    // zmienne do obslugi rozdzielczosci i trybu wyswietlania

    private int currentWidth;
    private int currentHeight;
    private FullScreenMode currentMode = FullScreenMode.FullScreenWindow;

    // zapis natywnej rozdzielczosci
    private int nativeWidth;
    private int nativeHeight;

    // obsluga przyciskow 

    public void NewGameBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsBtn()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OptionsBackBtn()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    //obluga zawartosci opcji

    void Start()
    {
        VolumeIni();
        PlayerPrefs.DeleteAll();
        Resolution natRes = Screen.currentResolution;
        currentWidth = Screen.width;
        currentHeight = Screen.height;
        currentMode = Screen.fullScreenMode;
        nativeWidth = natRes.width;
        nativeHeight = natRes.height;
    }

    public void Resolution(int index)
    {
        switch (index)
        {
            case 0:
                currentWidth = nativeWidth;
                currentHeight = nativeHeight;
                break;
            case 1:
                currentWidth = 640; currentHeight = 360;
                break;
            case 2:
                currentWidth = 854; currentHeight = 480;
                break;
            case 3:
                currentWidth = 1280; currentHeight = 720;
                break;
            case 4:
                currentWidth = 1920; currentHeight = 1080;
                break;
            case 5:
                currentWidth = 2560; currentHeight = 1440;
                break;
            case 6:
                currentWidth = 3840; currentHeight = 2160;
                break;
        }
        ApplyGraphics();
    }

    public void DisplayType(int index)
    {
        switch (index)
        {
            case 0:
                currentMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                currentMode = FullScreenMode.Windowed;
                break;
            case 2:
                currentMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }
        ApplyGraphics();
    }

    private void ApplyGraphics()
    {
        Screen.SetResolution(currentWidth, currentHeight, currentMode);
        Debug.Log($"Zastosowano: {currentWidth}x{currentHeight} w trybie {currentMode}");
    }

    void VolumeIni()
    {
        if (volumeSlider != null && volumeNumber != null)
        {
            UpdateVolumeDisplay(volumeSlider.value);
        }
    }

    public void UpdateVolumeDisplay(float value)
    {
        volumeNumber.text = value.ToString("0");
    }
}
