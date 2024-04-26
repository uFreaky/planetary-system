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

    private string nameInput = "";
    private float radiusInput = 500f;
    private float xVelocityInput = 0f;
    private float yVelocityInput = 0f;
    private float zVelocityInput = 0f;
    private int orbitsAroundInput = 0;

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
            string[] bodies = new string[currentPlanetarySystem.transform.childCount];
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i] = currentPlanetarySystem.transform.GetChild(i).GetComponent<AstronomicalBody>().name;
                GUILayout.Label(bodies[i]);
            }

            nameInput = GUILayout.TextField(nameInput);
            radiusInput = EditorGUILayout.FloatField(radiusInput);

            GUILayout.BeginHorizontal();
            xVelocityInput = EditorGUILayout.FloatField(xVelocityInput);
            yVelocityInput = EditorGUILayout.FloatField(yVelocityInput);
            zVelocityInput = EditorGUILayout.FloatField(zVelocityInput);
            GUILayout.EndHorizontal();

            orbitsAroundInput = EditorGUI.Popup(
                new Rect(0, 120, position.width, 20),
                "Orbits Around:",
                orbitsAroundInput,
                bodies);

            EditorGUILayout.Space(20f);

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
        if (IsSameName(name))
        {
            Debug.LogError("Planet name is already in use. Please choose a different name");
            return;
        }

        GameObject bodyObj = Instantiate((GameObject) Resources.Load("PlanetarySystem/prf_Planet"));
        bodyObj.transform.parent = currentPlanetarySystem.transform;
        AstronomicalBody astroBody = bodyObj.GetComponent<AstronomicalBody>();
        astroBody.name = name;
        bodyObj.name = name;
        astroBody.SetRadius(radius);
        astroBody.mass = SetMass(radius);

        astroBody.startVelocity = new Vector3 (xVelocityInput, yVelocityInput, zVelocityInput);

        astroBody.orbitsAround = currentPlanetarySystem.transform.GetChild(orbitsAroundInput).GetComponent<AstronomicalBody>();

        Repaint();

        Selection.activeGameObject = bodyObj;
        //creates prefab
        //puts in list
        //creates astroitem gui
    }

    private bool IsSameName(string name)
    {
        for (int i = 0; i < currentPlanetarySystem.transform.childCount; i++)
        {
            if (name.Equals(currentPlanetarySystem.transform.GetChild(i).name))
            {
                return true;
            }
        }
        return false;
    }

    private float SetMass(float radius)
    {
        //calculates volume of the sphere depending on the radius
        float volume = (4f / 3f) * Mathf.PI * (Mathf.Pow(radius, 3));

        //calculates the mass
        float mass = volume * currentPlanetarySystem.GetComponent<PhysicalLaw>().density;
        return mass;
    }

    private void LoadCreateScreen()
    {
        
    }

    private void LoadEditorScreen()
    {
        
    }
}
