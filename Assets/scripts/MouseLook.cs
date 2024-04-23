using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSens = 10f;

    private float mouseX;
    private float mouseY;
    private Vector2 mouseLookVec;

    [SerializeField] private Transform playerTf;

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

        Debug.Log(mouseLookVec.x + " - " + mouseLookVec.y);
    }

    private void Update()
    {
        if (mouseLookVec.magnitude >= 0.1f)
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
