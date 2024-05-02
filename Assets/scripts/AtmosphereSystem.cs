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

    private Material skyboxMat;

    private void Start()
    {
        skyboxMat = RenderSettings.skybox;
        skyboxMat.SetFloat("_HorizonHeight", 1.7f);

        StartCoroutine(UpdateHorizonState());
    }

    private IEnumerator UpdateHorizonState()
    {
        SetHorizonValues();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(UpdateHorizonState());
    }

    private void SetHorizonValues()
    {
        if (sunState < 0.17f)
        {
            float stateStrength = Mathf.InverseLerp(0f, 0.17f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState1, heightState2, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState1, reachState2, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(colorSate1, colorSate2, stateStrength));
        }
        else if (sunState < 0.33f)
        {
            float stateStrength = Mathf.InverseLerp(0.17f, 0.33f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState2, heightState3, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState2, reachState3, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(colorSate2, colorSate3, stateStrength));
        }
        else if (sunState < 0.5f)
        {
            float stateStrength = Mathf.InverseLerp(0.33f, 0.5f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState3, heightState4, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState3, reachState4, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(colorSate3, colorSate4, stateStrength));
        }
        else if (sunState < 0.67f)
        {
            float stateStrength = Mathf.InverseLerp(0.5f, 0.67f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState4, heightState5, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState4, reachState5, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(colorSate4, colorSate5, stateStrength));
        }
        else if (sunState < 0.83f)
        {
            float stateStrength = Mathf.InverseLerp(0.67f, 0.83f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState5, heightState6, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState5, reachState6, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(colorSate5, colorSate6, stateStrength));
        }
        else
        {
            float stateStrength = Mathf.InverseLerp(0.83f, 1f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState6, heightState7, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState6, reachState7, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(colorSate6, colorSate7, stateStrength));
        }
    }
}
