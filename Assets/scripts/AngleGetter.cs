using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleGetter : MonoBehaviour
{
    [SerializeField] private Transform sunUp;
    [SerializeField] private Transform sunDown;
    [SerializeField] private Transform cam;

    private void Start()
    {
        Vector3 sunUpDir = sunUp.position - cam.position;
        Vector3 sunDownDir = sunDown.position - cam.position;
        float angleSunUp = Vector3.Angle(sunUpDir, -cam.up);
        float angleSunDown = Vector3.Angle(sunDownDir, -cam.up);
        Debug.Log("AngleSunUp: " + angleSunUp);
        Debug.Log("AngleSunDown: " + angleSunDown);
    }
}
