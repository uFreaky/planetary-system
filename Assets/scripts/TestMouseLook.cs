using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouseLook : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public Transform yAxis;
    public float speed = 3;

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3(rotation.x, 0f, 0f) * speed;
        yAxis.localEulerAngles = new Vector3(0f, rotation.y, 0f) * speed;
    }
}