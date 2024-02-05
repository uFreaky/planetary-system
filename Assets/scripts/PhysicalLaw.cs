using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalLaw : MonoBehaviour
{
    public static PhysicalLaw instance;

    public float gravConst = 0.000000000066743f;

    //average density of the materials the astronomical bodies consist of
    public float density = 5513f;

    private void Awake()
    {
        instance = this;
    }

    public float PerfectOrbitInVelocity(float otherMass, float distance)
    {
        float velocitySqr = gravConst * otherMass * (1f / distance);
        return Mathf.Sqrt(velocitySqr);
    }
}
