using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AngleGetter : MonoBehaviour
{
    [SerializeField] private Transform sun;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform universe;

    private void Start()
    {
        Debug.Log("TESTA: " + cam.localEulerAngles.x);
    }

    private void Update()
    {
        Material skybox = RenderSettings.skybox;
        skybox.SetFloat("_xAxisOffset", -cam.localEulerAngles.x);
        skybox.SetFloat("_yAxisOffset", -cam.localEulerAngles.y);

        Vector3 sunDir = sun.position - player.position;
        //float sunAngle = Vector3.Angle(sunDir, universe.forward);
        float sunAngle = Vector3.SignedAngle(sunDir, -universe.forward, universe.up);

        Debug.Log("SUNANGLE: " + sunAngle + " : " + LeftRightCheck(-universe.forward, sunDir, universe.right));

        skybox.SetFloat("_sunOffset", sunAngle * LeftRightCheck(-universe.forward, sunDir, universe.right));

    }

    private float LeftRightCheck(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }
}
