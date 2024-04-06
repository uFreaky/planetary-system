using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetDetailUI : MonoBehaviour
{
    private RaycastHit hit;

    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask astroBodyLayer;

    [SerializeField] private RectTransform detailUiRect;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI distText;
    [SerializeField] private TextMeshProUGUI speedText;

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
            distText.text = (int)Vector2.Distance(hit.transform.position, cam.transform.position) + "m";
        }
        else
        {
            detailUiRect.gameObject.SetActive(false);
        }
    }
}
