using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverVitesse : MonoBehaviour
{
    public float vitesseLente = 0.5f, vitesseMoyenne = 1.2f, vitesseRapide = 2.0f;
    public float forceCran = 20f;
    public TreadmillsController mainController;

    private HingeJoint hinge;
    private XRGrabInteractable grab;

    // 0: Faible, 1: Moyenne, 2: Elevée, -1: Initialisation
    private int derniereVitesse = -1;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        grab = GetComponent<XRGrabInteractable>();

        // On s'assure que le moteur est éteint au départ pour permettre la saisie VR
        hinge.useMotor = false;
    }

    void Update()
    {
        float angle = hinge.angle;

        // Gestion du moteur pour le calage
        if (grab.isSelected)
        {
            hinge.useMotor = false; // Désactiver pour ne pas lutter contre la main
        }
        else
        {
            float target = 0f;
            if (angle < -22f) target = -45f;
            else if (angle > 22f) target = 45f;

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
                hinge.useMotor = false;
            }
        }

        // --- LOGIQUE DE VITESSE ET AFFICHAGE ---
        float speed = vitesseMoyenne;
        int vitesseActuelle = 1;
        string message = "Difficulté Moyenne";

        if (angle < -22f)
        {
            speed = vitesseLente;
            vitesseActuelle = 0;
            message = "Difficulté Faible";
        }
        else if (angle > 22f)
        {
            speed = vitesseRapide;
            vitesseActuelle = 2;
            message = "Difficulté Élevée";
        }

        // Si la vitesse a changé depuis la dernière image
        if (vitesseActuelle != derniereVitesse)
        {
            // Envoi au ScoreManager pour afficher la bulle
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.ShowDifficulty(message);
            }

            derniereVitesse = vitesseActuelle;
        }

        // Mise à jour physique et visuelle des tapis
        if (mainController != null)
        {
            mainController.SetSpeed(speed);
        }
    }
}