using UnityEngine;
using TMPro; // Important pour utiliser TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Permet aux autres scripts d'y accéder facilement
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        instance = this;
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