using UnityEngine;

public class Levier : MonoBehaviour
{
    public float maxDistance = 3f;
    public float vitesse = 5f;

    private Vector3 positionInitiale;

    void Start()
    {
        positionInitiale = transform.localPosition;
    }

    void OnMouseDrag()
    {
        float mouvement = Input.GetAxis("Mouse Y") * vitesse * Time.deltaTime;

        Vector3 pos = transform.localPosition;
        pos.z += mouvement;

        pos.z = Mathf.Clamp(
            pos.z,
            positionInitiale.y,
            positionInitiale.y + maxDistance
        );

        transform.localPosition = pos;
    }
}
