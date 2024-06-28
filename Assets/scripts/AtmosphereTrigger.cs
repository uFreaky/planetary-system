using System.Collections;
using UnityEngine;

public class AtmosphereTrigger : MonoBehaviour
{
    private Material skyboxMat;

    private float skyOpacity = 1f;
    [SerializeField] private Material starMat;

    private void Start()
    {
        skyboxMat = RenderSettings.skybox;

        skyboxMat.SetFloat("_SkyOpacity", skyOpacity);
        starMat.SetFloat("_SkyOpacity", skyOpacity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
            //other.transform.parent = transform.parent;

            StopAllCoroutines();
            StartCoroutine(FadeSky(true, 5f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
            //other.transform.parent = PlanetarySystem.instance.transform;

            StopAllCoroutines();
            StartCoroutine(FadeSky(false, 5f));
        }
    }

    public IEnumerator FadeSky(bool isFadingOn, float duration)
    {
        float startOpacity = skyboxMat.GetFloat("_SkyOpacity");

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            if (isFadingOn)
            {
                skyOpacity = Mathf.Lerp(startOpacity, 1f, t / duration);
            }
            else
            {
                skyOpacity = Mathf.Lerp(startOpacity, 0f, t / duration);
            }
            
            skyboxMat.SetFloat("_SkyOpacity", skyOpacity);
            starMat.SetFloat("_SkyOpacity", skyOpacity);

            yield return null;
        }
    }
}
