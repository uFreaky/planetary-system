using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereSystem : MonoBehaviour
{
    public float SunState
    {
        get { return sunState; }
        set { sunState = Mathf.Clamp(value, 0f, 1f); }
    }
    [SerializeField, Range(0f, 1f)] private float sunState;

    [SerializeField] private float heightState1 = 0f;
    [SerializeField] private float heightState2 = 1.28f;
    [SerializeField] private float heightState3 = 1.28f;
    [SerializeField] private float heightState4 = 1.5f;
    [SerializeField] private float heightState5 = 1.7f;
    [SerializeField] private float heightState6 = 1.7f;
    [SerializeField] private float heightState7 = 1.7f;

    [SerializeField] private float reachState1 = 0f;
    [SerializeField] private float reachState2 = 1.77f;
    [SerializeField] private float reachState3 = 3.11f;
    [SerializeField] private float reachState4 = 3.11f;
    [SerializeField] private float reachState5 = 0.96f;
    [SerializeField] private float reachState6 = 0.5f;
    [SerializeField] private float reachState7 = 0f;

    [SerializeField] private Color colorSate1;
    [SerializeField] private Color colorSate2;
    [SerializeField] private Color colorSate3;
    [SerializeField] private Color colorSate4;
    [SerializeField] private Color colorSate5;
    [SerializeField] private Color colorSate6;
    [SerializeField] private Color colorSate7;

    private void Start()
    {
        RenderSettings.skybox.SetFloat("_HorizonHeight", 1.7f);
    }

    private void Update()
    {
        
    }
}
