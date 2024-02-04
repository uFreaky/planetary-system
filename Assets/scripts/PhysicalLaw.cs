using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalLaw : MonoBehaviour
{
    public static PhysicalLaw instance;

    public float gravConst = 0.000000000066743f;

    private void Awake()
    {
        instance = this;
    }
}
