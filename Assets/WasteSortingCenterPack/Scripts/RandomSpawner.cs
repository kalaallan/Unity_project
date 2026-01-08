using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 1f;
    public float spawnDistance = 1f;

    private void Start()
    {
        // Supprimé : StartSpawning() n'est plus appelé ici 
        // C'est le GameLauncher qui s'en chargera quand on clique sur "Jouer"
    }

    private void SpawnInFront()
    {
        // Sécurité supplémentaire : si le temps est arrêté, on ne spawn rien
        if (Time.timeScale == 0) return;

        if (objectsToSpawn.Length == 0) return;

        GameObject randomPrefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Vector3 spawnPos = transform.position + transform.right * spawnDistance;
        GameObject obj = Instantiate(randomPrefab, spawnPos, Quaternion.identity);

        // On s'assure que le tag est bien mis pour la détection des points
        obj.tag = "Spawned";
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnInFront));
    }

    public void StartSpawning()
    {
        if (!IsInvoking(nameof(SpawnInFront)))
        {
            // Le premier spawn arrivera après 'spawnInterval' secondes
            InvokeRepeating(nameof(SpawnInFront), spawnInterval, spawnInterval);
        }
    }
}