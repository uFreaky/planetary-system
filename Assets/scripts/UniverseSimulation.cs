using UnityEngine;

public class UniverseSimulation : MonoBehaviour
{
    [SerializeField] private int framesPerSecond = 30;
    private float timePassed;
    private float timeInterval;

    private void Start()
    {
        timePassed = 0f;
        timeInterval = 1f / framesPerSecond;

        /**
        //testing goin on=
        foreach (AstronomicalBody body in GameManager.instance.currentAstronomicalBodies)
        {
            //nimm bei accel später das vector3.distance sqr von unten anstatt radius sqr
            float distance = Vector3.Distance(body.otherBody.transform.position, body.transform.position);
            float acceleration = (PhysicalLaw.instance.gravConst * body.otherBody.mass) / (body.otherBody.radius * body.otherBody.radius);
            Vector3 direction = (body.otherBody.transform.position - body.transform.position).normalized;
            body.velocity += (direction * acceleration) / framesPerSecond;

            Debug.Log("accel: " + (PhysicalLaw.instance.gravConst * body.otherBody.mass) / (body.otherBody.radius * body.otherBody.radius));

            Debug.Log("dist: " + Vector3.Distance(body.otherBody.transform.position, body.transform.position));
            Debug.Log("dir: " + direction);
            Debug.Log("velocity: " + (direction * acceleration) / framesPerSecond);

            body.transform.position += body.velocity;
        }**/

        //Debug.Log((PhysicalLaw.instance.gravConst * 5.972e24f) / (6371000f * 6371000f));
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        //Gets called the number of framesPerSecond times in a second.
        if (timePassed >= timeInterval)
        {
            timePassed -= timeInterval;

            foreach (AstronomicalBody body in GameManager.instance.currentAstronomicalBodies)
            {
                //nimm bei accel später das vector3.distance sqr von unten anstatt radius sqr
                float distance = Vector3.Distance(body.otherBody.transform.position, body.transform.position);
                float acceleration = (PhysicalLaw.instance.gravConst * body.otherBody.mass) / (body.otherBody.radius * body.otherBody.radius);
                Vector3 direction = (body.otherBody.transform.position - body.transform.position).normalized;
                body.velocity += (direction * acceleration) / framesPerSecond;

                Debug.Log("accel: " + (PhysicalLaw.instance.gravConst * body.otherBody.mass) / (body.otherBody.radius * body.otherBody.radius));

                Debug.Log("dist: " + Vector3.Distance(body.otherBody.transform.position, body.transform.position));
                Debug.Log("dir: " + direction);
                Debug.Log("velocity: " + (direction * acceleration) / framesPerSecond);

                body.transform.position += body.velocity;
            }
        }
    }
}
