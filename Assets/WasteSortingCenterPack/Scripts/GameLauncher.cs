using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public GameObject menuCanvas;
    public TreadmillsController treadmillController;
    public RandomSpawner spawner;
    public TimerManager timerManager; // Référence au gestionnaire de temps

    void Start()
    {
        Time.timeScale = 1f;

        if (menuCanvas != null) menuCanvas.SetActive(true);

        if (treadmillController != null)
        {
            treadmillController.SetPaused(true);
            treadmillController.SetSpeed(0);
        }
        if (spawner != null) spawner.StopSpawning();

        FreezeObjects(true);
    }

    public void LancerLeJeu()
    {
        if (menuCanvas != null) menuCanvas.SetActive(false);

        FreezeObjects(false);

        if (treadmillController != null) treadmillController.SetPaused(false);
        if (spawner != null) spawner.StartSpawning();

        // On lance le compte à rebours
        if (timerManager != null) timerManager.StartTimer();

        Debug.Log("Le jeu commence !");
    }

    // Nouvelle fonction pour tout stopper à la fin du temps
    public void StopGame()
    {
        if (treadmillController != null)
        {
            treadmillController.SetPaused(true);
            treadmillController.SetSpeed(0);
        }
        if (spawner != null) spawner.StopSpawning();

        FreezeObjects(true);
        Debug.Log("Temps écoulé, jeu arrêté.");
    }

    void FreezeObjects(bool freeze)
    {
        GameObject[] spawned = GameObject.FindGameObjectsWithTag("Spawned");
        foreach (GameObject g in spawned)
        {
            Rigidbody rb = g.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = freeze;
        }
    }
}