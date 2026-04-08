using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScoreDisplay;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}