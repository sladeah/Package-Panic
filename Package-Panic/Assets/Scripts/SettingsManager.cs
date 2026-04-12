using UnityEngine;
using System.IO;

[System.Serializable]
public class SettingsData
{
    public float masterVolume = 1.0f;
}

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public SettingsData currentSettings;
    private string savePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "packagepanic_settings.json");
        
        LoadSettings();
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(currentSettings, true);
        File.WriteAllText(savePath, json);
        
        AudioListener.volume = currentSettings.masterVolume;
    }

    public void LoadSettings()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentSettings = JsonUtility.FromJson<SettingsData>(json);
        }
        else
        {
            currentSettings = new SettingsData();
            SaveSettings();
        }

        AudioListener.volume = currentSettings.masterVolume;
    }
}