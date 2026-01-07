using UnityEngine;
using System.Collections;

public class EmergencyHandle : MonoBehaviour
{
    [Header("Paramètres")]
    public float threshold = 0.15f;
    public float pauseDuration = 5f;
    public float stretchMultiplier = 10f;
    public float maxScaleY = 2.0f; // Limite visuelle de l'étirement

    [Header("Références")]
    public TreadmillsController controller;

    private Vector3 initialLocalPos;
    private Vector3 initialScale;
    private bool triggerActivated = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialLocalPos = transform.localPosition;
        initialScale = transform.localScale;

        if (controller == null)
            controller = Object.FindFirstObjectByType<TreadmillsController>();
    }

    void Update()
    {
        // 1. Calcul de la distance de tirage réelle
        float pullDistance = Mathf.Abs(transform.localPosition.y - initialLocalPos.y);

        // 2. Étirement visuel progressif limité
        float newScaleY = Mathf.Min(initialScale.y + (pullDistance * stretchMultiplier), maxScaleY);
        transform.localScale = new Vector3(initialScale.x, newScaleY, initialScale.z);

        // 3. Déclenchement de l'urgence
        if (pullDistance >= threshold && !triggerActivated)
        {
            StartCoroutine(TriggerEmergencyAndReset());
        }
    }

    IEnumerator TriggerEmergencyAndReset()
    {
        triggerActivated = true;
        if (controller != null) controller.SetPaused(true);
        Debug.Log("URGENCE : Tapis stoppé !");

        // Attente des 5 secondes pendant que l'utilisateur lâche ou tient la poignée
        yield return new WaitForSeconds(pauseDuration);

        // 4. Retour à la position initiale
        // On rend le Rigidbody cinématique un court instant pour le "téléporter" au début
        rb.isKinematic = true;
        transform.localPosition = initialLocalPos;
        transform.localScale = initialScale;
        rb.isKinematic = false;

        if (controller != null) controller.SetPaused(false);
        triggerActivated = false;
        Debug.Log("Tapis redémarré et poignée réinitialisée.");
    }
}