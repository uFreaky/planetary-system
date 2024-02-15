using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalBody : MonoBehaviour
{
    public float mass;
    public float radius;
    public Vector3 startVelocity;
    public Vector3 velocity;
    public Rigidbody rb;

    [SerializeField] private bool isPerfectOrbit = false;

    public AstronomicalBody orbitsAround = null;
    public Vector3 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetRadius(radius);

        //might be able to set orbit direction here for perfect orbit function too
        //keep in mind perfect orbit might not work with multiple close planets, so use mainly for testing
        if (isPerfectOrbit)
        {
            float distance = Vector3.Distance(orbitsAround.transform.position, transform.position);
            startVelocity *= PhysicalLaw.instance.PerfectOrbitInVelocity(orbitsAround.mass, distance);
            velocity = startVelocity;

            rb.velocity = velocity;
        }
        else
        {
            velocity = startVelocity;
        }
    }

    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        transform.localScale = 2 * radius * Vector3.one;

        SetMass(newRadius);
    }

    private void SetMass(float radius)
    {
        //calculates volume of the sphere depending on the radius
        float volume = (4f/3f) * Mathf.PI * (Mathf.Pow(radius, 3));

        //calculates the mass
        mass = volume * PhysicalLaw.instance.density;
        rb.mass = mass;
    }

    public float CalculateAcceleration()
    {
        return 0f;
    }
}
