using System.Collections;
using UnityEngine;

public class AtmosphereSystem : MonoBehaviour
{
    public float SunState
    {
        get { return sunState; }
        set { sunState = Mathf.Clamp(value, 0f, 1f); }
    }
    [SerializeField, Range(0f, 1f)] private float sunState;

    [SerializeField] private Transform player;
    [SerializeField] private Transform sun;
    [SerializeField] private Transform cam;
    [SerializeField] private float sunUpAngle = 118f;
    [SerializeField] private float sunDownAngle = 61f;

    private Material skyboxMat;
    [SerializeField] private Material starMat;

    [SerializeField] private float heightState1 = 0f;
    [SerializeField] private float heightState2 = 0.035f;
    [SerializeField] private float heightState3 = 0.25f;
    [SerializeField] private float heightState4 = 0.7f;
    [SerializeField] private float heightState5 = 1.53f;
    [SerializeField] private float heightState6 = 1.7f;
    [SerializeField] private float heightState7 = 1.7f;

    [SerializeField] private float reachState1 = 0f;
    [SerializeField] private float reachState2 = 0.9f;
    [SerializeField] private float reachState3 = 4.75f;
    [SerializeField] private float reachState4 = 5.26f;
    [SerializeField] private float reachState5 = 1f;
    [SerializeField] private float reachState6 = 0.23f;
    [SerializeField] private float reachState7 = 0f;

    [SerializeField] private Color sunColorSate1 = new(255f, 193f, 0f);
    [SerializeField] private Color sunColorSate2 = new(255f, 193f, 0f);
    [SerializeField] private Color sunColorSate3 = new(236f, 97f, 10f);
    [SerializeField] private Color sunColorSate4 = new(255f, 0f, 0f);
    [SerializeField] private Color sunColorSate5 = new(236f, 97f, 10f);
    [SerializeField] private Color sunColorSate6 = new(255f, 193f, 0f);
    [SerializeField] private Color sunColorSate7 = new(255f, 193f, 0f);
    [SerializeField] private Color midColorState1 = Color.black;
    [SerializeField] private Color midColorState2 = new(80f, 175f, 228f);
    [SerializeField] private Color topColorState1 = Color.black;
    [SerializeField] private Color topColorState2 = new(80f, 175f, 228f);

    private void Start()
    {
        skyboxMat = RenderSettings.skybox;
        //cam = Camera.main.transform;

        //test
        //SunState = 0.83f;

        StartCoroutine(UpdateHorizonState());

        //StartCoroutine(ChangeSomeValue(0f, 1f, 60f));
    }

    private void Update()
    {
        skyboxMat.SetFloat("_xAxisOffset", -cam.localEulerAngles.x);
        skyboxMat.SetFloat("_yAxisOffset", -cam.localEulerAngles.y);
    }

    private IEnumerator UpdateHorizonState()
    {
        SetHorizonValues();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(UpdateHorizonState());
    }

    private void SetHorizonValues()
    {
        UpdateSunState();
        UpdateSunOffset();

        if (sunState < 0.17f)
        {
            float stateStrength = Mathf.InverseLerp(0f, 0.17f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState1, heightState2, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState1, reachState2, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate1, sunColorSate2, stateStrength));
            starMat.SetFloat("_Opacity", Mathf.Abs(sunState * 2f - 1f));
        }
        else if (sunState < 0.33f)
        {
            float stateStrength = Mathf.InverseLerp(0.17f, 0.33f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState2, heightState3, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState2, reachState3, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate2, sunColorSate3, stateStrength));
            starMat.SetFloat("_Opacity", Mathf.Abs(sunState * 2f - 1f));
        }
        else if (sunState < 0.5f)
        {
            float stateStrength = Mathf.InverseLerp(0.33f, 0.5f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState3, heightState4, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState3, reachState4, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate3, sunColorSate4, stateStrength));
            starMat.SetFloat("_Opacity", Mathf.Abs(sunState * 2f -1f));
        }
        else if (sunState < 0.67f)
        {
            float stateStrength = Mathf.InverseLerp(0.5f, 0.67f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState4, heightState5, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState4, reachState5, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate4, sunColorSate5, stateStrength));
        }
        else if (sunState < 0.83f)
        {
            float stateStrength = Mathf.InverseLerp(0.67f, 0.83f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState5, heightState6, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState5, reachState6, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate5, sunColorSate6, stateStrength));
        }
        else
        {
            float stateStrength = Mathf.InverseLerp(0.83f, 1f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState6, heightState7, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState6, reachState7, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate6, sunColorSate7, stateStrength));
            skyboxMat.SetColor("_BotColor", Color.Lerp(sunColorSate6, topColorState2, stateStrength));
        }

        skyboxMat.SetColor("_MidColor2", Color.Lerp(midColorState1, midColorState2, sunState));
        skyboxMat.SetColor("_TopColor", Color.Lerp(topColorState1, topColorState2, sunState));
    }

    private void UpdateSunState()
    {
        Vector3 sunDir = sun.position - player.position;
        float sunAngle = Vector3.Angle(sunDir, -player.up);
        SunState = Mathf.InverseLerp(sunDownAngle, sunUpAngle, sunAngle);
    }

    private void UpdateSunOffset()
    {
        Vector3 sunDir = sun.position - player.position;
        //float sunAngle = Vector3.Angle(sunDir, universe.forward);
        float sunAngle = Vector3.SignedAngle(sunDir, -transform.forward, transform.up);

        Debug.Log("SUNANGLE: " + sunAngle + " : " + LeftRightCheck(-transform.forward, sunDir, transform.right));

        skyboxMat.SetFloat("_sunOffset", sunAngle * LeftRightCheck(-transform.forward, sunDir, transform.right));
    }

    private float LeftRightCheck(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }

    //test function to slowly lerp from one value to another, can be deleted later on
    public IEnumerator ChangeSomeValue(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            SunState = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        SunState = newValue;
    }
}