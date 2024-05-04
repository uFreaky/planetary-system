using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldSystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform starfield;

    //THIS SCRIPT NEEDS TO: handle communication between atmosphere script and starfield to make it fade when atmosphere is entered or day/night
    private void Update()
    {
        starfield.position = player.position;
    }
}
