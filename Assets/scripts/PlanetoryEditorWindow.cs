using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetoryEditorWindow : EditorWindow
{
    private bool isSelected = false;

    [MenuItem("Window/Planetory System Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlanetoryEditorWindow>("Planetory System Editor");
    }

    private void OnGUI()
    {
        if (isSelected)
        {
            
        }
        GUILayout.Label("SYSTEM SELECTED: " + isSelected);
    }

    private void CreateAstronomicalBody()
    {

    }

    private void LoadCreateScreen()
    {

    }

    private void LoadEditorScreen()
    {

    }

    private bool IsSelectPlanetory()
    {
        return false;
    }
}
