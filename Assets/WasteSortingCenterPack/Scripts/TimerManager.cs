using UnityEngine;
using TMPro; // Nécessaire pour afficher le texte

public class TimerManager : MonoBehaviour
{
    [Header("Configuration")]
    public float timeRemaining = 60f; // Diminué à 60 secondes
    public bool timerIsRunning = false;

    [Header("UI")]
    public TextMeshProUGUI timerText; // Glisse ton texte UI ici
    public GameObject winnerCanvas;   // Canvas de victoire
    public GameObject gameOverCanvas; // Canvas de défaite

    [Header("Références")]
    public GameLauncher gameLauncher;

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Time.deltaTime fait décroître le temps selon la vitesse réelle des secondes
                timeRemaining -= Time.deltaTime;
                UpdateDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                CheckEndGame();
            }
        }
    }

    void UpdateDisplay(float timeToDisplay)
    {
        // On s'assure que le texte ne devienne pas négatif à l'affichage
        float t = Mathf.Max(0, timeToDisplay);
        float minutes = Mathf.FloorToInt(t / 60);
        float seconds = Mathf.FloorToInt(t % 60);

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void CheckEndGame()
    {
        // Accès au score via l'instance du ScoreManager
        int scoreFinal = ScoreManager.instance != null ? ScoreManager.instance.score : 0;

        if (scoreFinal > 0)
        {
            if (winnerCanvas != null) winnerCanvas.SetActive(true);
        }
        else
        {
            if (gameOverCanvas != null) gameOverCanvas.SetActive(true);
        }

        // On appelle le GameLauncher pour arrêter la simulation
        if (gameLauncher != null) gameLauncher.StopGame();
    }
}