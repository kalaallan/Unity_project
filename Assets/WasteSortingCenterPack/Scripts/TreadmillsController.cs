using UnityEngine;

public class TreadmillsController : MonoBehaviour
{
    [SerializeField] Material treadmillMat;
    TreadmillForce[] treadmills;
    const float MATERIAL_SPEED_MULTIPLIER = 1f;

    [Header("Pause State")]
    public bool isPaused { get; private set; }
    private float lastRequestedSpeed; // On garde en mémoire la vitesse voulue par le levier

    void Start()
    {
        treadmills = FindObjectsByType<TreadmillForce>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        // On force l'arrêt visuel au démarrage au cas où le shader aurait une valeur résiduelle
        if (treadmillMat != null) treadmillMat.SetFloat("_Speed", 0);
    }

    public void SetSpeed(float newSpeed)
    {
        // On stocke la vitesse demandée par le levier, même en pause
        lastRequestedSpeed = newSpeed;

        // Si on est en pause, on ignore la demande et on force tout à zéro
        if (isPaused)
        {
            ApplyToHardware(0);
        }
        else
        {
            ApplyToHardware(newSpeed);
        }
    }

    public void SetPaused(bool value)
    {
        isPaused = value;

        if (isPaused)
        {
            ApplyToHardware(0); // Stop immédiat
        }
        else
        {
            ApplyToHardware(lastRequestedSpeed); // Reprise à la vitesse du levier
        }
    }

    // Cette fonction interne s'occupe de l'exécution physique et visuelle
    private void ApplyToHardware(float speed)
    {
        if (treadmills != null)
        {
            foreach (TreadmillForce t in treadmills)
            {
                if (t != null) t.SetSpeed(speed);
            }
        }

        if (treadmillMat != null)
        {
            treadmillMat.SetFloat("_Speed", speed * MATERIAL_SPEED_MULTIPLIER);
        }
    }
}