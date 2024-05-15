using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AstronomicalBody : MonoBehaviour
{
    public new string name;
    public float mass;
    public float radius;
    public float distance;//delete
    public Vector3 xDir;//delete
    public Vector3 yDir;//delete
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
        SetMass(radius, PhysicalLaw.instance.density);

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
    }

    public void SetMass(float radius, float density)
    {
        //calculates volume of the sphere depending on the radius
        float volume = (4f/3f) * Mathf.PI * (Mathf.Pow(radius, 3));

        //calculates the mass
        mass = volume * density;
        rb.mass = mass;
    }

    public float CalculateAcceleration()
    {
        return 0f;
    }
}
