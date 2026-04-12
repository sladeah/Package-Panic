using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Slider volumeSlider;
    private bool isPaused = false;

    private void Start()
    {
        volumeSlider.value = SettingsManager.Instance.currentSettings.masterVolume;
        
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

   public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (!isPaused)
        {
            SettingsManager.Instance.SaveSettings();
        }
    }

private void UpdateVolume(float newVolume)
    {
        SettingsManager.Instance.currentSettings.masterVolume = newVolume;
        AudioListener.volume = newVolume; 
        
    }

   public void QuitMatch()
    {
        int currentScore = GameManager.Instance.GetScore(); 
        
        DatabaseManager.Instance.SaveMatchScore(currentScore);
        
        int bestScore = DatabaseManager.Instance.GetHighScore();
        Debug.Log("Your All-Time High Score is: " + bestScore);

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
        }
        Application.Quit();
    }
}