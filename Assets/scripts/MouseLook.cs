using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens = 10f;

    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;
    private Vector2 mouseLookVec;

    [SerializeField] private Transform cameraY;
    [SerializeField] private Transform cam;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnLook(InputValue value)
    {
        mouseLookVec = value.Get<Vector2>().normalized;
        mouseX = mouseLookVec.x;
        mouseY = mouseLookVec.y;
    }

    private void Update()
    {
        if (mouseLookVec.magnitude >= 0.1f)
        {
            Debug.Log("looking: " + transform.TransformDirection(transform.up) * (mouseX * mouseSens * Time.deltaTime));

            cameraY.Rotate(new Vector3(0f, 1f, 0f) * (mouseX * mouseSens * Time.deltaTime), Space.Self);
            xRotation -= mouseY * mouseSens * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
