using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSettingsView _audioSettingsView;

    public float MasterVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float SFXVolume { get; private set; }

    private void OnEnable()
    {
        if (_audioSettingsView != null)
        {
            _audioSettingsView.MasterVolume.onValueChanged.AddListener(SetMasterVolume);
            _audioSettingsView.MusicVolume.onValueChanged.AddListener(SetMusicVolume);
            _audioSettingsView.SfxVolume.onValueChanged.AddListener(SetSfxVolume);
        }
    }

    private void OnDisable()
    {
        if (_audioSettingsView != null)
        {
            _audioSettingsView.MasterVolume.onValueChanged.RemoveAllListeners();
            _audioSettingsView.MusicVolume.onValueChanged.RemoveAllListeners();
            _audioSettingsView.SfxVolume.onValueChanged.RemoveAllListeners();
        }
    }

    private void Start()
    {
        if (!TryLoadData())
            InitializeFromMixer();
    }

    #region Setters

    public void SetMasterVolume(float value)
    {
        MasterVolume = Mathf.Clamp01(value);
        _audioMixer.SetFloat("Master", DenormalizeSlider(MasterVolume));
        UpdateUI();
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = Mathf.Clamp01(value);
        _audioMixer.SetFloat("Music", DenormalizeSlider(MusicVolume));
        UpdateUI();
    }

    public void SetSfxVolume(float value)
    {
        SFXVolume = Mathf.Clamp01(value);
        _audioMixer.SetFloat("SFX", DenormalizeSlider(SFXVolume));
        UpdateUI();
    }

    #endregion

    #region Initialization

    private bool TryLoadData()
    {
        AudioSettingsDTO settings = SaveSystem.Load()?.audioSettings;

        if (settings != null)
        {
            MasterVolume = settings.masterVolume;
            MusicVolume = settings.musicVolume;
            SFXVolume = settings.sfxVolume;

            _audioMixer.SetFloat("Master", DenormalizeSlider(MasterVolume));
            _audioMixer.SetFloat("Music", DenormalizeSlider(MusicVolume));
            _audioMixer.SetFloat("SFX", DenormalizeSlider(SFXVolume));

            UpdateUI();
            return true;
        }

        return false;
    }

    private void InitializeFromMixer()
    {
        MasterVolume = 0.5f;
        MusicVolume = 1f;
        SFXVolume = 1f;

        _audioMixer.SetFloat("Master", DenormalizeSlider(MasterVolume));
        _audioMixer.SetFloat("Music", DenormalizeSlider(MusicVolume));
        _audioMixer.SetFloat("SFX", DenormalizeSlider(SFXVolume));

        UpdateUI();
    }


    #endregion

    #region Utilities

    private void UpdateUI()
    {
        if (_audioSettingsView == null) return;

        _audioSettingsView.MasterVolume.SetValueWithoutNotify(MasterVolume);
        _audioSettingsView.MusicVolume.SetValueWithoutNotify(MusicVolume);
        _audioSettingsView.SfxVolume.SetValueWithoutNotify(SFXVolume);
    }

    private float DenormalizeSlider(float value) => Mathf.Lerp(-80f, 0f, Mathf.Clamp01(value));

    #endregion
}