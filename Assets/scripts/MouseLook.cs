using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens = 10f;

    private float mouseX;
    private float mouseY;

    [SerializeField] private Transform playerTf;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnLook(InputValue value)
    {
        Vector2 lookVector2 = value.Get<Vector2>().normalized;
        mouseX = lookVector2.x;
        mouseY = lookVector2.y;

        Debug.Log(lookVector2.x + " - " + lookVector2.y);
    }

    private void Update()
    {
        if (mouseX >= 0.1f || mouseY >= 0.1f)
        {
            Debug.Log("looking");

            playerTf.Rotate(transform.TransformDirection(transform.up) * (mouseX * mouseSens * Time.deltaTime));
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
