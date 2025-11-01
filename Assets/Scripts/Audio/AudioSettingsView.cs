using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsView : MonoBehaviour
{
    [field: SerializeField] public Slider MasterVolume { get; private set; }
    [field: SerializeField] public Slider MusicVolume { get; private set; }
    [field: SerializeField] public Slider SfxVolume { get; private set; }

    public void UpdateUI(float masterVolume, float musicVolume, float sfxVolume)
    {
        MasterVolume.value = masterVolume;
        MusicVolume.value = musicVolume;
        SfxVolume.value = sfxVolume;
    }
}
