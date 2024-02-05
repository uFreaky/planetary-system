using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalBody : MonoBehaviour
{
    public float mass;
    public float radius;
    [SerializeField] private Vector3 startVelocity;
    public Vector3 velocity;
    private Rigidbody rb;

    [SerializeField] private bool isPerfectOrbit = false;

    //for testing
    public AstronomicalBody otherBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetRadius(radius);

        //might be able to set orbit direction here for perfect orbit function too
        //keep in mind perfect orbit might not work with multiple close planets, so use mainly for testing
        if (isPerfectOrbit)
        {
            float distance = Vector3.Distance(otherBody.transform.position, transform.position);
            startVelocity *= PhysicalLaw.instance.PerfectOrbitInVelocity(otherBody.mass, distance);
            velocity = startVelocity;
        }
        else
        {
            velocity = startVelocity;
        }
    }

    private void SetRadius(float newRadius)
    {
        radius = newRadius;
        transform.localScale = 2 * radius * Vector3.one;

        SetMass(newRadius);
    }

    private void SetMass(float radius)
    {
        //calculates volume of the sphere depending on the radius
        float volume = (4f/3f) * Mathf.PI * (Mathf.Pow(radius, 3));
        Debug.Log(volume);

        //calculates the mass
        mass = volume * PhysicalLaw.instance.density;
    }

    public float CalculateAcceleration()
    {
        return 0f;
    }
}
