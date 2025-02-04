using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Assign in Unity Inspector

    private static bool isInitialized = false; // Prevents duplicate instances

    private void Awake()
    {
        if (!isInitialized)
        {
            DontDestroyOnLoad(gameObject); // Keeps this object across scenes
            isInitialized = true;
        }
        else
        {
            Destroy(gameObject); // Prevents multiple VolumeManagers
        }
    }

    private void Start()
    {
        // Load saved volume setting
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("GameVolume");
            AudioListener.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            volumeSlider.value = AudioListener.volume; // Default to current volume
        }

        // Add a listener to detect when the slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Adjust global volume
        PlayerPrefs.SetFloat("GameVolume", volume); // Save setting
        PlayerPrefs.Save();
    }
}
