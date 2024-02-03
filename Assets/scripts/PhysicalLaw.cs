using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalLaw : MonoBehaviour
{
    [SerializeField] private float gravitationalConstant = 0.000000000066743f;

    public float GravitationalConstant()
    {
        return gravitationalConstant;
    }
}
