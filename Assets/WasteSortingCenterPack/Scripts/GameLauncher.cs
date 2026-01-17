using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLauncher : MonoBehaviour
{
    public GameObject menuCanvas;    // Menu du début (5s)
    public GameObject replayCanvas;  // Menu de fin (5s)
    public TreadmillsController treadmillController;
    public RandomSpawner spawner;
    public TimerManager timerManager;

    void Start()
    {
        Time.timeScale = 1f;

        // Configuration initiale : Menu début ON, Menu fin OFF
        if (menuCanvas != null) menuCanvas.SetActive(true);
        if (replayCanvas != null) replayCanvas.SetActive(false);

        InitialiserJeu(true);

        // ÉTAPE 1 : Attendre 5s au début avant de lancer
        StartCoroutine(AttendreEtLancer(5f));
    }

    IEnumerator AttendreEtLancer(float delai)
    {
        yield return new WaitForSeconds(delai);
        LancerLeJeu();
    }

    public void LancerLeJeu()
    {
        if (menuCanvas != null) menuCanvas.SetActive(false);
        InitialiserJeu(false);
        if (timerManager != null) timerManager.StartTimer();
    }

    // ÉTAPE 2 : Quand le temps est fini, on affiche "Rejouer" pendant 5s
    public void StopGame()
    {
        InitialiserJeu(true);

        // On affiche le menu de fin
        if (replayCanvas != null) replayCanvas.SetActive(true);

        // On lance le décompte automatique pour relancer la scène
        StartCoroutine(AttendreEtRelancer(5f));
    }

    IEnumerator AttendreEtRelancer(float delai)
    {
        yield return new WaitForSeconds(delai);

        // Optionnel : on cache le menu juste avant de charger
        if (replayCanvas != null) replayCanvas.SetActive(false);

        RejouerLeJeu();
    }

    void InitialiserJeu(bool enPause)
    {
        if (treadmillController != null)
        {
            treadmillController.SetPaused(enPause);
            if (enPause) treadmillController.SetSpeed(0);
        }
        if (spawner != null)
        {
            if (enPause) spawner.StopSpawning();
            else spawner.StartSpawning();
        }
        FreezeObjects(enPause);
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

    public void RejouerLeJeu()
    {
        // Recharge la scène actuelle pour tout remettre à zéro
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}