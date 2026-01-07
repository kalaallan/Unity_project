using UnityEngine;

public class LeverVitesse : MonoBehaviour
{
    [Header("Réglage de la vitesse")]
    public TreadmillForce treadmill;
    public float minSpeed = 0f;
    public float maxSpeed = 5f;

    [Header("Rotation du levier (local X)")]
    public float minAngle = -30f;
    public float maxAngle = 30f;

    private void Update()
    {
        // Récupère l'angle actuel du levier
        float angle = transform.localEulerAngles.x;

        // Convertit l'angle en -180/+180 pour éviter les valeurs 0->360
        if (angle > 180f) angle -= 360f;

        // Normalise angle -> 0..1
        float t = Mathf.InverseLerp(minAngle, maxAngle, angle);

        // Calcule la vitesse correspondante
        float newSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);

        // Envoie la vitesse au tapis
        treadmill.SetSpeed(newSpeed);
    }
}
