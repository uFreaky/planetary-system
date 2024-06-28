using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldSystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform spaceship;
    [SerializeField] private Transform starfield;

    //THIS SCRIPT NEEDS TO: handle communication between atmosphere script and starfield to make it fade when atmosphere is entered or day/night
    private void Update()
    {
        if (player.gameObject.activeSelf)
        {
            starfield.position = player.position;
        }
        else
        {
            starfield.position = spaceship.position;
        }
    }
}
