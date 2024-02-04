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

    //for testing
    public AstronomicalBody otherBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        velocity = startVelocity;
    }

    public float CalculateAcceleration()
    {
        return 0f;
    }
}
