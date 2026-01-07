using UnityEngine;

public class TreadmillsController : MonoBehaviour
{
    [SerializeField] Material treadmillMat;
    TreadmillForce[] treadmills;
    const float MATERIAL_SPEED_MULTIPLIER = 1f;

    [Header("Pause")]
    public bool isPaused { get; private set; }
    private float currentSpeed;

    void Start()
    {
        // On récupère tous les tapis
        treadmills = FindObjectsByType<TreadmillForce>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    // On supprime l'Update pour ne plus écraser la vitesse du levier

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = isPaused ? 0 : newSpeed;

        // On applique la vitesse physique à chaque tapis
        foreach (TreadmillForce t in treadmills)
        {
            if (t != null) t.SetSpeed(currentSpeed);
        }

        // On applique la vitesse visuelle au tapis (shader)
        if (treadmillMat != null)
        {
            treadmillMat.SetFloat("_Speed", currentSpeed * MATERIAL_SPEED_MULTIPLIER);
        }
    }

    public void SetPaused(bool value)
    {
        isPaused = value;
        // On rafraîchit la vitesse actuelle (0 si pause)
        SetSpeed(currentSpeed);
    }
}