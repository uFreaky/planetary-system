using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform testPlanet; //hardcoded

    [SerializeField] private CharacterController controller;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    public bool isGrounded = false;

    private float gravity; //needs to be calculated depending on current planet and how far away you are.
    [SerializeField] private float jumpHeight = 3f;
    private Vector3 velocity;

    Controls controls;

    //input direction
    private Vector3 direction;

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        //test hard coded:
        gravity = -9.81f;
    }

    private void OnMovement(InputValue value)
    {
        Debug.Log("oh brother");
        Vector2 dirVector2 = value.Get<Vector2>();
        direction = new Vector3(dirVector2.x, 0f, dirVector2.y).normalized;
    }

    private void Update()
    {
        //Vector3 direction = (testPlanet.transform.position - transform.position).normalized;
        //transform.rotation = Quaternion.Euler(direction);
        //Debug.Log(Quaternion.Euler(direction));
        transform.LookAt(testPlanet.position);
        transform.Rotate(new Vector3(-90f, 0f, 0f));

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded) //needs to check if on way down or up (newly jumped), otherwise this turns true right after jumping later
        {
            velocity = -2f * transform.up;
        }
        else
        {
            velocity += gravity * Time.deltaTime * transform.up;
            controller.Move(velocity * Time.deltaTime);
        }

        /**
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Debug.Log(velocity);**/

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction.normalized * Time.deltaTime);
        }
    }

    private void Jump()
    {
        Debug.Log("Jumped");
        if (isGrounded)
        {
            velocity = transform.up * jumpHeight;
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
