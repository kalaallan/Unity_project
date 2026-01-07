using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; // Nécessaire pour détecter le grab

public class EmergencyHandle : MonoBehaviour
{
    [Header("Paramètres")]
    public float threshold = 0.15f;
    public float pauseDuration = 5f;
    public float stretchMultiplier = 10f;
    public float maxScaleY = 2.0f;

    [Header("Références")]
    public TreadmillsController controller;
    public RandomSpawner spawner;

    private Vector3 initialLocalPos;
    private Vector3 initialScale;
    private bool triggerActivated = false;
    private bool isGrabbed = false; // Pour savoir si on tient l'objet
    private Rigidbody rb;
    private XRGrabInteractable interactable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialLocalPos = transform.localPosition;
        initialScale = transform.localScale;

        // Configuration auto des références
        interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(x => isGrabbed = true);
            interactable.selectExited.AddListener(x => isGrabbed = false);
        }

        if (controller == null)
            controller = Object.FindFirstObjectByType<TreadmillsController>();

        if (spawner == null)
            spawner = Object.FindFirstObjectByType<RandomSpawner>();
    }

    void Update()
    {
        if (triggerActivated) return;

        float pullDistance = Mathf.Abs(transform.localPosition.y - initialLocalPos.y);

        // Si on tire, on étire
        if (isGrabbed)
        {
            float newScaleY = Mathf.Min(initialScale.y + (pullDistance * stretchMultiplier), maxScaleY);
            transform.localScale = new Vector3(initialScale.x, newScaleY, initialScale.z);

            if (pullDistance >= threshold)
            {
                StartCoroutine(TriggerEmergencyAndReset());
            }
        }
        // Si on lâche et que l'urgence n'est pas active, on remet à zéro
        else if (transform.localScale != initialScale)
        {
            ResetHandle();
        }
    }

    IEnumerator TriggerEmergencyAndReset()
    {
        triggerActivated = true;

        if (controller != null) controller.SetPaused(true);
        if (spawner != null) spawner.StopSpawning();

        yield return new WaitForSeconds(pauseDuration);

        if (controller != null) controller.SetPaused(false);
        if (spawner != null) spawner.StartSpawning();

        ResetHandle();
        triggerActivated = false;
    }

    void ResetHandle()
    {
        // On coupe temporairement la physique pour replacer l'objet sans collision violente
        if (rb != null) rb.isKinematic = true;

        transform.localScale = initialScale;
        transform.localPosition = initialLocalPos;

        if (rb != null) rb.isKinematic = false;
    }
}