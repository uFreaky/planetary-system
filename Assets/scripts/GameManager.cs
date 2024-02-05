using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [SerializeField] private float testTime = 1f;

    //temporary for testing, later needs to be filled as you create AstronomicalBody objects in the editor.
    public AstronomicalBody[] currentAstronomicalBodies;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Time.timeScale = testTime;
    }
}
