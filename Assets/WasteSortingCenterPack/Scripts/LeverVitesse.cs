using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverVitesse : MonoBehaviour
{
    public float vitesseLente = 0.5f, vitesseMoyenne = 1.2f, vitesseRapide = 2.0f;
    public float forceCran = 20f;
    public TreadmillForce[] allTreadmills;
    public TreadmillsController mainController;

    private HingeJoint hinge;
    private XRGrabInteractable grab;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        grab = GetComponent<XRGrabInteractable>();

        // On s'assure que le moteur est éteint au départ
        hinge.useMotor = false;

        if (allTreadmills == null || allTreadmills.Length == 0)
            allTreadmills = Object.FindObjectsByType<TreadmillForce>(FindObjectsSortMode.None);
    }

    void Update()
    {
        float angle = hinge.angle;

        // Si on tient le levier, le moteur doit être éteint pour ne pas bloquer la main
        if (grab.isSelected)
        {
            hinge.useMotor = false;
        }
        else
        {
            // LOGIQUE DE CALAGE :
            // Si le levier n'est pas déjà parfaitement sur un cran, on active le moteur
            float target = 0f;
            if (angle < -22f) target = -45f;
            else if (angle > 22f) target = 45f;

            // Si on est à plus de 1 degré de la cible, on active le moteur pour "caler"
            if (Mathf.Abs(angle - target) > 1f)
            {
                hinge.useMotor = true;
                JointMotor motor = hinge.motor;
                motor.force = forceCran;
                motor.targetVelocity = (target - angle) * 10f;
                hinge.motor = motor;
            }
            else
            {
                // Une fois calé, on coupe le moteur pour libérer la physique
                hinge.useMotor = false;
            }
        }

        // Mise à jour de la vitesse des tapis
        float speed = vitesseMoyenne;
        if (angle < -20f) speed = vitesseLente;
        else if (angle > 20f) speed = vitesseRapide;

        if (mainController != null)
        {
            mainController.SetSpeed(speed);
        }
    }
}