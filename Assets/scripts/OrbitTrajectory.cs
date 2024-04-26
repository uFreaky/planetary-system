using System;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitTrajectory : MonoBehaviour
{
    [SerializeField] private int stepsToDraw = 1000;
    [SerializeField] private bool showOrbits = true;
    [SerializeField] private Color color = Color.black;

    private Vector3 lastPosition;

    private AstronomicalBody[] ghostBodies;

    [SerializeField] private float framesPerSecond = 30f;
    private float timePassed;
    private float timeInterval;

    [SerializeField] private Transform ghostBodiesParent;

    //THIS BOOL IS TEMP!!! later when anything in editor window changes that also changes the orbits, the SimulateAndDrawGizmos method should be called with that and prolly not in ondrawgizmos. 
    [SerializeField] private bool isDrawn = false;

    private void Start()
    {
        timePassed = 0f;
        timeInterval = 1f / framesPerSecond;

        DeleteGhostBodies();
    }

    private void Update()
    {
        if (!Application.isPlaying || showOrbits)
        {
            DrawTrajectories();
        }
    }

    private void DrawTrajectories()
    {
        DeleteGhostBodies();

        AstronomicalBody[] bodies = new AstronomicalBody[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            bodies[i] = transform.GetChild(i).GetComponent<AstronomicalBody>();
        }

        ghostBodies = new AstronomicalBody[bodies.Length];

        for (int i = 0; i < bodies.Length; i++)
        {
            ghostBodies[i] = Instantiate(bodies[i], ghostBodiesParent, true);
            ghostBodies[i].GetComponent<Renderer>().enabled = false;
            //ghostBodies[i].SetRadius(ghostBodies[i].radius);
            ghostBodies[i].velocity = ghostBodies[i].startVelocity;

            LineRenderer lineRenderer = ghostBodies[i].GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
            lineRenderer.positionCount = stepsToDraw;
        }

        for (int i = 0; i < stepsToDraw; i++)
        {
            SimulateAndDrawLine(i);
        }
    }

    private void SimulateAndDrawLine(int step)
    {
        timePassed += Time.deltaTime;

        //Gets called the number of framesPerSecond times in a second.
        if (timePassed >= timeInterval)
        {
            timePassed -= timeInterval;

            foreach (AstronomicalBody body in ghostBodies)
            {
                if (step == 0)
                {
                    body.startPosition = body.transform.position;
                }

                foreach (AstronomicalBody otherBody in ghostBodies)
                {
                    if (body != otherBody)
                    {
                        float distance = Vector3.Distance(otherBody.transform.position, body.transform.position);
                        //grav constant is hardcoded here, change later
                        float acceleration = (0.005f * otherBody.mass) / (distance * distance);
                        Vector3 direction = (otherBody.transform.position - body.transform.position).normalized;
                        body.velocity += (direction * acceleration) / framesPerSecond;

                        LineRenderer lineRenderer = body.GetComponent<LineRenderer>();
                        if (body.orbitsAround != null)
                        {
                            float orbitsAroundDist = Vector3.Distance(otherBody.transform.position, otherBody.startPosition);
                            Vector3 orbitsAroundDir = (otherBody.transform.position - otherBody.startPosition).normalized;
                            Vector3 orbitsAroundDiff = orbitsAroundDist * orbitsAroundDir;
                            lineRenderer.SetPosition(step, body.transform.position - orbitsAroundDiff);
                        }
                        else
                        {
                            lineRenderer.SetPosition(step, body.transform.position);
                        }

                        //lastPosition = body.transform.position;
                        body.transform.position += body.velocity;
                    }
                }
            }
        }
    }

    private void DeleteGhostBodies()
    {
        var tempChildren = ghostBodiesParent.transform.Cast<Transform>().ToList();
        foreach (Transform child in tempChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}