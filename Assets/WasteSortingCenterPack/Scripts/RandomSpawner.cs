using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;   // Les objets possibles
    public float spawnInterval = 1f;      // 1 seconde entre chaque spawn
    public float spawnDistance = 1f;      // Distance en avant (axe X)

    private void Start()
    {
        // Répéter la fonction toutes les "spawnInterval" secondes
        InvokeRepeating(nameof(SpawnInFront), 0f, spawnInterval);
    }

    private void SpawnInFront()
    {
        if (objectsToSpawn.Length == 0) return;

        // Choisir un prefab aléatoire
        GameObject randomPrefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Calculer la position en avant du spawner (sur l’axe X local)
        Vector3 spawnPos = transform.position + transform.right * spawnDistance;

        // Créer l’objet à cette position
        GameObject obj = Instantiate(randomPrefab, spawnPos, Quaternion.identity);
        obj.tag = "Spawned";
    }
}
