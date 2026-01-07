using UnityEngine;

public class dropobjet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spawned"))
        {
            Destroy(other.gameObject);
        }
    }
}
