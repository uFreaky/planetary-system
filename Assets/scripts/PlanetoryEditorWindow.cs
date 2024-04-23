using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetaryEditorWindow : EditorWindow
{
    private PlanetarySystem currentPlanetarySystem = null;
    private PlanetarySystem newPlanetarySystem = null;

    private bool isCreateScreen = true;

    private List<AstronomicalBody> bodies = new List<AstronomicalBody>();

    private string nameInput = "";
    private float radiusInput = 500f;

    [MenuItem("Window/Planetary System Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlanetaryEditorWindow>("Planetary System Editor");
    }

    private void OnGUI()
    {
        BuildUi();

        if (isCreateScreen)
        {
            GUILayout.Label("Universe Creator");
            if (GUILayout.Button("Create New Universe"))
            {
                CreateNewUniverse();
            }
        }
        else
        {
            foreach (AstronomicalBody body in bodies)
            {
                GUILayout.Label(body.name);
            }

            nameInput = GUILayout.TextField(nameInput);
            radiusInput = EditorGUILayout.FloatField(radiusInput);

            if (GUILayout.Button("Create Planet"))
            {
                CreateAstronomicalBody(nameInput, radiusInput);
            }
        }
    }

    private void OnSelectionChange()
    {
        Repaint();
    }

    private void BuildUi()
    {
        if (Selection.activeGameObject == null)
        {
            return;
        }

        //Debug.Log(Selection.activeGameObject.name);

        newPlanetarySystem = Selection.activeGameObject.GetComponentInParent<PlanetarySystem>();
        if (newPlanetarySystem != null)
        {
            if (ReferenceEquals(currentPlanetarySystem, newPlanetarySystem))
            {
                return;
            }
            else
            {
                currentPlanetarySystem = newPlanetarySystem;
                isCreateScreen = false;
            }
        }
        else
        {
            if (currentPlanetarySystem != null)
            {
                currentPlanetarySystem = newPlanetarySystem;
                isCreateScreen = true;
            }
        }
    }

    private void CreateNewUniverse()
    {
        GameObject universePref = Instantiate((GameObject) Resources.Load("PlanetarySystem/PlanetarySystem"));
    }

    private void CreateAstronomicalBody(string name, float radius)
    {
        GameObject bodyObj = Instantiate((GameObject) Resources.Load("PlanetarySystem/prf_Planet"));
        AstronomicalBody astroBody = bodyObj.GetComponent<AstronomicalBody>();
        astroBody.name = name;
        astroBody.radius = radius;
        bodies.Add(astroBody);
        Repaint();
        //creates prefab
        //puts in list
        //creates astroitem gui
    }

    private void LoadCreateScreen()
    {
        
    }

    private void LoadEditorScreen()
    {
        
    }
}
