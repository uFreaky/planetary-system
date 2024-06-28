using UnityEngine;

public class UniverseOriginSetter : MonoBehaviour
{
    [SerializeField] private float maxDistance = 1500f;

    public Rigidbody player;
    public Rigidbody shipRb;
    public Transform ship;
    [SerializeField] private Transform cam;

    private void LateUpdate()
    {
        Vector3 distanceOffset = player.position;
        float distanceFromCenter = distanceOffset.magnitude;

        if (distanceFromCenter > maxDistance)
        {
            transform.position -= distanceOffset;
            if (!player.gameObject.activeSelf)
            {
                shipRb.position -= distanceOffset;
            }
            player.position = Vector3.zero;
        }
    }
}
