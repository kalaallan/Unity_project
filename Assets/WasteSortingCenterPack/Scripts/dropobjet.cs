using UnityEngine;

public class dropobjet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spawned"))
        {
            // Règle : Seul le Carton 1 doit aller ici (+1)
            if (other.name.Contains("Carton0"))
            {
                ScoreManager.instance.AjouterPoint();
                Debug.Log("Carton1 bien trié dans le trou ! +1");
            }
            else
            {
                // Carton 2 ou Bouteille ici est une erreur (-1)
                ScoreManager.instance.RetirerPoint();
                Debug.Log("Erreur : Mauvais objet dans le trou ! -1");
            }

            Destroy(other.gameObject);
        }
    }
}