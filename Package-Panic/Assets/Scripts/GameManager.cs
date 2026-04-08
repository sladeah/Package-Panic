using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public delegate void ScoreChangedDelegate(int newScore);
    public static event ScoreChangedDelegate OnScoreChanged;

    public NetworkVariable<int> matchTime = new NetworkVariable<int>(180);
    
    public NetworkVariable<int> playerScore = new NetworkVariable<int>(0);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void OnNetworkSpawn()
    {
        playerScore.OnValueChanged += (int previousValue, int newValue) =>
        {
            OnScoreChanged?.Invoke(newValue);
        };
    }

    public void AddScore(int points)
    {
        if (IsServer)
        {
            playerScore.Value += points;
        }
    }
}