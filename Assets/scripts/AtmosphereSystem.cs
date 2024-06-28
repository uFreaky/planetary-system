using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform shipCam;
    [SerializeField] private float sunUpAngle = 118f;
    [SerializeField] private float sunDownAngle = 61f;
    [SerializeField] private Vector3 skyboxSunDir = new(-1f, 0f, 0f);
    [SerializeField] private Vector3 leftRightDir = new(0f, 0f, -1f);
    [SerializeField] private Vector3 worldUpDir = new(0f, 1f, 0f);

    private Material skyboxMat;
    [SerializeField] private Material starMat;
    [SerializeField] private Material astroMat;
    [SerializeField] private Material sunMat;

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

        starMat.renderQueue = 4000;
        skyboxMat.renderQueue = 4001;
        astroMat.renderQueue = 0;
        sunMat.renderQueue = 0;

        StartCoroutine(UpdateHorizonState());
    }

    private void Update()
    {
        skyboxMat.SetFloat("_xAxisOffset", -playerCam.localEulerAngles.x);
        UpdateSunOffset();
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

        if (sunState < 0.17f)
        {
            float stateStrength = Mathf.InverseLerp(0f, 0.17f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState1, heightState2, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState1, reachState2, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate1, sunColorSate2, stateStrength));
            skyboxMat.SetColor("_BotColor", Color.Lerp(topColorState1, sunColorSate6, stateStrength));
            starMat.SetFloat("_Opacity", Mathf.Abs(sunState * 2f - 1f));
        }
        else if (sunState < 0.33f)
        {
            float stateStrength = Mathf.InverseLerp(0.17f, 0.33f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState2, heightState3, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState2, reachState3, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate2, sunColorSate3, stateStrength));
            skyboxMat.SetColor("_BotColor", sunColorSate6);
            starMat.SetFloat("_Opacity", Mathf.Abs(sunState * 2f - 1f));
        }
        else if (sunState < 0.5f)
        {
            float stateStrength = Mathf.InverseLerp(0.33f, 0.5f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState3, heightState4, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState3, reachState4, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate3, sunColorSate4, stateStrength));
            skyboxMat.SetColor("_BotColor", sunColorSate6);
            starMat.SetFloat("_Opacity", Mathf.Abs(sunState * 2f -1f));
        }
        else if (sunState < 0.67f)
        {
            float stateStrength = Mathf.InverseLerp(0.5f, 0.67f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState4, heightState5, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState4, reachState5, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate4, sunColorSate5, stateStrength));
            skyboxMat.SetColor("_BotColor", sunColorSate6);
        }
        else if (sunState < 0.83f)
        {
            float stateStrength = Mathf.InverseLerp(0.67f, 0.83f, sunState);
            skyboxMat.SetFloat("_HorizonHeight", Mathf.Lerp(heightState5, heightState6, stateStrength));
            skyboxMat.SetFloat("_HorizonReach", Mathf.Lerp(reachState5, reachState6, stateStrength));
            skyboxMat.SetColor("_MidColorSun", Color.Lerp(sunColorSate5, sunColorSate6, stateStrength));
            skyboxMat.SetColor("_BotColor", sunColorSate6);
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
        Vector3 projectionSunDir = Vector3.ProjectOnPlane(sunDir, player.up);
        float sunAngle;
        if (player.gameObject.activeSelf)
        {
            sunAngle = Vector3.SignedAngle(projectionSunDir, playerCam.parent.forward, player.up);
        }
        else
        {
            sunAngle = Vector3.SignedAngle(projectionSunDir, shipCam.parent.forward, player.up);
        }
        skyboxMat.SetFloat("_sunOffset", -sunAngle + 90f);
    }

    private void UpdateSunOffsetDeprecated()
    {
        Vector3 sunDir = sun.position - player.position;
        sunDir.x = 0f;
        //float sunAngle = Vector3.Angle(sunDir, universe.forward);
        //check later if cam.parent.forward is still correct way to do this
        float sunAngle = Vector3.SignedAngle(sunDir, playerCam.parent.forward, player.up);

        Debug.Log("SUNANGLE: " + sunAngle + " : " + LeftRightCheck(player.forward, sunDir, player.up));
        Debug.Log("SunDir: " + sunDir);

        skyboxMat.SetFloat("_sunOffset", -sunAngle + 90f);
    }

    //deprecated?
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