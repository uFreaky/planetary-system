using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDetailUI : MonoBehaviour
{
    private RaycastHit hit;

    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask astroBodyLayer;

    [SerializeField] private Transform detailUi;

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, astroBodyLayer))
        {
            detailUi.gameObject.SetActive(true);
        }
        else
        {
            detailUi.gameObject.SetActive(false);
        }
    }
}
