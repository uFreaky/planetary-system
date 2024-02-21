using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetaryEditorWindow : EditorWindow
{
    private bool isSelected = false;

    private List<AstronomicalBody> bodies = new List<AstronomicalBody>();

    [MenuItem("Window/Planetary System Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlanetaryEditorWindow>("Planetary System Editor");
    }

    private void OnGUI()
    {
        if (isSelected)
        {
            
        }
        GUILayout.Label("SYSTEM SELECTED: " + isSelected);

        CreateStartGUI();
        CreateAstroItems();
    }

    private void CreateStartGUI()
    {
        if (GUILayout.Button("CREATE"))
        {
            CreateAstronomicalBody();
        }
    }

    private void CreateAstroItems()
    {
        foreach (AstronomicalBody body in bodies)
        {
            GUILayout.Label(body.gameObject.name);
        }
    }

    private void CreateAstronomicalBody()
    {
        GameObject bodyObj = Instantiate((GameObject) Resources.Load("Prefabs/prf_Planet"));
        bodies.Add(bodyObj.GetComponent<AstronomicalBody>());
        OnGUI();
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

    private bool IsSelectPlanetory()
    {
        return false;
    }
}
