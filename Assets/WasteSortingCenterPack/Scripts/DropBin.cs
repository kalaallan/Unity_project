using UnityEngine;

public class DropBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est un de nos objets de jeu
        if (other.CompareTag("Spawned"))
        {
            // Règle : Carton 1 dans la poubelle est une erreur (-1)
            if (other.name.Contains("carton1"))
            {
                ScoreManager.instance.RetirerPoint();
                Debug.Log("Erreur : Carton1 dans la poubelle ! -1");
            }
            else
            {
                // Tout le reste (Carton 2, Bouteille) donne +1
                ScoreManager.instance.AjouterPoint();
                Debug.Log("Bon tri ! +1");
            }

            Destroy(other.gameObject);
        }
    }
}