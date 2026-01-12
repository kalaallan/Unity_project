using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI difficultyText;
    public int score = 0;

    void Awake() { instance = this; }

    // Cette fonction gère maintenant la couleur et l'affichage permanent
    public void ShowDifficulty(string message)
    {
        difficultyText.text = message;
        difficultyText.gameObject.SetActive(true);

        // Logique de couleur selon le texte reçu
        if (message.Contains("Faible") || message.Contains("Facile"))
        {
            difficultyText.color = Color.white; // Couleur blanche
        }
        else if (message.Contains("Moyenne"))
        {
            difficultyText.color = Color.green; // Couleur verte
        }
        else
        {
            difficultyText.color = Color.red; // Couleur rouge
        }
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