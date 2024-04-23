using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySystem : MonoBehaviour
{
    [HideInInspector] public static PlanetarySystem instance;

    public int framesPerSecond = 30;

    //temporary for testing, later needs to be filled as you create AstronomicalBody objects in the editor.
    public AstronomicalBody[] currentAstronomicalBodies;

    private void Awake()
    {
        instance = this;
    }
}
