using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Toggle bgmToggle;
    public Toggle fxToggle;

    public Button openButton;
    public Button closeButton;

    public Slider bgmSlider;
    public Slider fxSlider;

    public GameObject panel;

    private void Awake()
    {
        bgmToggle.onValueChanged.AddListener(OnBGMToggleChange);
        fxToggle.onValueChanged.AddListener(OnFXToggleChange);

        openButton.onClick.AddListener(OpenOptionPanel);
        closeButton.onClick.AddListener(CloseOptionPanel);

        bgmSlider.onValueChanged.AddListener(OnBGMSliderChange);
        fxSlider.onValueChanged.AddListener(OnFxSliderChange);
    }

    private void OnBGMToggleChange(bool isOn)
    {
        SoundManager.Instance.OnOffBGM(isOn);
    }

    private void OnFXToggleChange(bool isOn)
    {
        SoundManager.Instance.OnOffFx(isOn);
    }

    private void OnBGMSliderChange(float volume)
    {
        SoundManager.Instance.ChangeBGMVolume(volume);
    }

    private void OnFxSliderChange(float volume)
    {
        SoundManager.Instance.ChangeFxVolume(volume);
    }

    private void OpenOptionPanel()
    {
        panel.SetActive(true);
    }

    private void CloseOptionPanel()
    {
        panel.SetActive(false);
    }
}
