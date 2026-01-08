using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EmergencyHandle : MonoBehaviour
{
    [Header("Paramètres")]
    public float threshold = 0.15f;
    public float pauseDuration = 5f;
    public float forceRetour = 50f; // Force quand on lâche

    [Header("Références")]
    public TreadmillsController controller;
    public RandomSpawner spawner;

    private ConfigurableJoint joint;
    private XRGrabInteractable grab;
    private Vector3 initialLocalPos;
    private bool isExecuting = false;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        grab = GetComponent<XRGrabInteractable>();
        initialLocalPos = transform.localPosition;
    }

    void Update()
    {
        // On vérifie la distance d'étirement
        float distance = Vector3.Distance(transform.localPosition, initialLocalPos);

        if (grab != null && grab.isSelected)
        {
            // PENDANT QU'ON TIRE : 
            // On baisse le ressort mais on ne le met pas à 0 pour garder une petite tension
            SetSpringForce(5f);
        }
        else
        {
            // QUAND ON LÂCHE :
            // On remet la force pour qu'il revienne en place
            SetSpringForce(forceRetour);
        }

        // Détection de l'activation
        if (distance > threshold && !isExecuting)
        {
            StartCoroutine(EmergencyStopRoutine());
        }
    }

    void SetSpringForce(float force)
    {
        if (joint != null)
        {
            JointDrive drive = joint.yDrive;
            drive.positionSpring = force;
            joint.yDrive = drive;
        }
    }

    System.Collections.IEnumerator EmergencyStopRoutine()
    {
        isExecuting = true;
        if (controller != null) controller.SetPaused(true);
        if (spawner != null) spawner.StopSpawning();

        yield return new WaitForSeconds(pauseDuration);

        if (controller != null) controller.SetPaused(false);
        if (spawner != null) spawner.StartSpawning();
        isExecuting = false;
    }
}