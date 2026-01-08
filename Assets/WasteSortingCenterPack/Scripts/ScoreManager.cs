using UnityEngine;
using TMPro;
using System.Collections; // Nécessaire pour les Coroutines

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI difficultyText; // Glisse ton nouveau texte ici
    private int score = 0;

    void Awake() { instance = this; }

    public void ShowDifficulty(string message)
    {
        StopAllCoroutines(); // Arrête l'ancienne notification si on change vite
        StartCoroutine(DisplayRoutine(message));
    }

    IEnumerator DisplayRoutine(string message)
    {
        difficultyText.text = message;
        difficultyText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); // Temps d'affichage (2 secondes)

        difficultyText.gameObject.SetActive(false);
    }

    public void AjouterPoint()
    {
        score++;
        UpdateUI();
    }

    public void RetirerPoint()
    {
        score--;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score = " + score;
    }
}