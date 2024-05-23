using UnityEngine;

public class UniverseOriginSetter : MonoBehaviour
{
    [SerializeField] private float maxDistance = 1500f;

    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;

    private void LateUpdate()
    {
        Vector3 distanceOffset = cam.position;
        float distanceFromCenter = distanceOffset.magnitude;

        if (distanceFromCenter > maxDistance)
        {
            transform.position -= distanceOffset;
            //Transform currentPlanet = player.parent;
            //player.parent = null;
            player.GetComponent<CharacterController>().enabled = false;
            player.position = Vector3.zero;
            player.GetComponent<CharacterController>().enabled = true;
            Debug.Log("PAUSED");
            Debug.Break();
        }
    }
}
