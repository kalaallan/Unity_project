using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public GameObject menuCanvas;
    public TreadmillsController treadmillController;
    public RandomSpawner spawner;

    void Start()
    {
        // On s'assure que le temps s'écoule (Time.timeScale = 1) 
        // pour que les manettes et rayons bougent normalement
        Time.timeScale = 1f;

        if (menuCanvas != null) menuCanvas.SetActive(true);

        // On stoppe les machines et la génération
        if (treadmillController != null)
        {
            treadmillController.SetPaused(true);
            treadmillController.SetSpeed(0);
        }
        if (spawner != null) spawner.StopSpawning();

        // On fige les objets déjà sur le tapis pour qu'ils ne tombent pas
        FreezeObjects(true);
    }

    public void LancerLeJeu()
    {
        if (menuCanvas != null) menuCanvas.SetActive(false);

        // On libère la physique des objets
        FreezeObjects(false);

        if (treadmillController != null) treadmillController.SetPaused(false);
        if (spawner != null) spawner.StartSpawning();

        Debug.Log("Le jeu commence via manettes VR !");
    }

    void FreezeObjects(bool freeze)
    {
        GameObject[] spawned = GameObject.FindGameObjectsWithTag("Spawned");
        foreach (GameObject g in spawned)
        {
            Rigidbody rb = g.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = freeze; // isKinematic fige l'objet sans arrêter le temps
        }
    }
}