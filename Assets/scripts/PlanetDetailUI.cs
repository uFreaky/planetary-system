using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetDetailUI : MonoBehaviour
{
    private RaycastHit hit;

    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask astroBodyLayer;

    [SerializeField] private RectTransform detailUiRect;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI distText;
    [SerializeField] private TextMeshProUGUI veloText;

    [SerializeField] private CanvasScaler canvas;

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, astroBodyLayer))
        {
            detailUiRect.gameObject.SetActive(true);

            float multiplier = canvas.referenceResolution.x / Screen.width;

            Vector3 detailScreenPos = cam.WorldToScreenPoint(hit.transform.position);
            detailUiRect.anchoredPosition = new Vector2(detailScreenPos.x * multiplier, detailScreenPos.y * multiplier);

            AstronomicalBody body = hit.transform.GetComponent<AstronomicalBody>();
            nameText.text = "Name: " + body.name;
            distText.text = (int)Vector3.Distance(hit.transform.position, cam.transform.position) + "m";
            if (body.velocity.magnitude >= 1)
            {
                veloText.text = (int)body.velocity.magnitude + "m/s";
            }
            else
            {
                veloText.text = body.velocity.magnitude.ToString("F2") + "m/s";
            }
        }
        else
        {
            detailUiRect.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(1);
        }
    }
}
