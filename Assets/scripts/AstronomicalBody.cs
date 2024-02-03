using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalBody : MonoBehaviour
{
    [SerializeField] private int mass;
    [SerializeField] private int radius;
    private float velocity;
    private Rigidbody rb;

    //for testing
    [SerializeField] private AstronomicalBody otherBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float CalculateAcceleration()
    {
        return 0f;
    }
}
