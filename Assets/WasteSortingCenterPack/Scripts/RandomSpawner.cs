using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 1f;
    public float spawnDistance = 1f;

    private void Start()
    {
        StartSpawning(); 
    }

    private void SpawnInFront()
    {
        if (objectsToSpawn.Length == 0) return;
        GameObject randomPrefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Vector3 spawnPos = transform.position + transform.right * spawnDistance;
        GameObject obj = Instantiate(randomPrefab, spawnPos, Quaternion.identity);
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
            InvokeRepeating(nameof(SpawnInFront), spawnInterval, spawnInterval);
        }
    }
}