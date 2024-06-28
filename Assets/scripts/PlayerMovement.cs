using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform testPlanet; //hardcoded
    private AstronomicalBody currentPlanet;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody shipRb;

    [SerializeField] private Transform cam;
    [SerializeField] private Transform cameraY;

    //movement speed
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravRotateSpeed = 40f;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    public bool isGrounded = false;
    private bool justJumped = false;

    private float gravity; //needs to be calculated depending on current planet and how far away you are.
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpCooldown = 0.5f;
    private Vector3 velocity;

    [SerializeField] private Transform spaceship;

    private Controls controls;

    //input direction
    private Vector2 direction;

    private void Awake()
    {
        controls = new Controls();

        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.EnterShip.performed += ctx => EnterShip();
    }

    private void Start()
    {
        //test hard coded:
        gravity = -9.81f;

        Cursor.lockState = CursorLockMode.Locked;

        currentPlanet = testPlanet.GetComponent<AstronomicalBody>();

        //TEST HARDCODED
        transform.parent = testPlanet;

        Destroy(spaceship.GetComponent<Rigidbody>());
    }

    private void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>().normalized;
    }

    private void Update()
    {
        Vector3 gravUp = (transform.position - testPlanet.position).normalized;
        Vector3 playerUp = transform.up;
        Quaternion targetRot = Quaternion.FromToRotation(playerUp, gravUp) * transform.rotation;
        transform.rotation = targetRot;

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && !justJumped)
        {
            velocity = -2f * gravUp;
        }
        else
        {
            velocity += gravity * 500f * Time.deltaTime * gravUp;
        }

        rb.velocity = velocity * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = new Vector3(direction.x, 0f, direction.y);
            rb.velocity += speed * 500f * Time.deltaTime * cameraY.TransformDirection(moveDir);
        }
    }

    private void FixedUpdate()
    {
        /*float distance = Vector3.Distance(currentPlanet.transform.position, transform.position);
        float accel = (PhysicalLaw.instance.gravConst * currentPlanet.mass) / (distance * distance);
        Vector3 dir = (currentPlanet.transform.position - transform.position).normalized;
        rb.velocity = dir * accel / 30f;*/
    }

    private void Jump()
    {
        Debug.Log("Jumped");
        if (isGrounded)
        {
            StartCoroutine(JustJumped());
            velocity = 500f * jumpHeight * transform.up;
        }
    }

    private IEnumerator JustJumped()
    {
        justJumped = true;

        yield return new WaitForSeconds(jumpCooldown);

        justJumped = false;
    }

    private void EnterShip()
    {
        controls.Player.Disable();
        spaceship.GetComponent<ShipMovement>().enabled = true;
        GetComponent<PlayerInput>().enabled = false;
        spaceship.GetComponent<PlayerInput>().enabled = true;
        cam.GetComponent<Camera>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;
        spaceship.GetComponent<MouseLook>().enabled = true;
        gameObject.SetActive(false);
        spaceship.GetComponent<PlanetDetailUI>().enabled = true;
        spaceship.GetComponent<ShipMovement>().rb = spaceship.AddComponent<Rigidbody>();
        spaceship.GetComponent<ShipMovement>().rb.mass = 2000f;
        PlanetarySystem.instance.GetComponent<UniverseOriginSetter>().shipRb = spaceship.GetComponent<ShipMovement>().rb;
        enabled = false;
    }

    private void OnEnable()
    {
        controls.Spaceship.Disable();
        controls.Player.Enable();

        cam.GetComponent<Camera>().enabled = true;
        GetComponentInChildren<MouseLook>().enabled = true;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Spaceship.Enable();
    }
}
