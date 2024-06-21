using UnityEngine;

public class UniverseSimulation : MonoBehaviour
{
    private float framesPerSecond;
    private float timePassed;
    private float timeInterval;

    private void Start()
    {
        timePassed = 0f;
        framesPerSecond = PlanetarySystem.instance.framesPerSecond;
        timeInterval = 1f / framesPerSecond;
    }

    private void FixedUpdate()
    {
        timePassed += Time.deltaTime;

        //Gets called the number of framesPerSecond times in a second.
        if (timePassed >= timeInterval)
        {
            timePassed -= timeInterval;

            foreach (AstronomicalBody body in PlanetarySystem.instance.currentAstronomicalBodies)
            {
                foreach (AstronomicalBody otherBody in PlanetarySystem.instance.currentAstronomicalBodies)
                {
                    if (otherBody != body)
                    {
                        float distance = Vector3.Distance(otherBody.transform.position, body.transform.position);
                        float acceleration = (PhysicalLaw.instance.gravConst * otherBody.mass) / (distance * distance);
                        Vector3 direction = (otherBody.transform.position - body.transform.position).normalized;

                        body.velocity += (direction * acceleration) / framesPerSecond;
                        body.transform.position += body.velocity;
                    }
                }
            }
        }
    }

    /**
    private void FixedUpdate()
    {
        timePassed += Time.deltaTime;

        //Gets called the number of framesPerSecond times in a second.
        if (timePassed >= timeInterval)
        {
            timePassed -= timeInterval;

            foreach (AstronomicalBody body in GameManager.instance.currentAstronomicalBodies)
            {
                float distance = Vector3.Distance(body.otherBody.transform.position, body.transform.position);
                Vector3 direction = (body.otherBody.transform.position - body.transform.position).normalized;
                float force = PhysicalLaw.instance.gravConst * ((body.mass * body.otherBody.mass) / Mathf.Pow(distance, 2));
                Vector3 forceVector = force * direction;

                body.rb.AddForce(forceVector);

                Debug.Log(body.gameObject.name + ":");
                Debug.Log("dist: " + Vector3.Distance(body.otherBody.transform.position, body.transform.position));
                Debug.Log("dir: " + direction);
                Debug.Log("force: " + force);
                Debug.Log("velo: " + body.rb.velocity);
            }
        }
    }**/
}
