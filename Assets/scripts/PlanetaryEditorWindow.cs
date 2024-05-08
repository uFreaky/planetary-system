using UnityEditor;
using UnityEngine;

public class PlanetaryEditorWindow : EditorWindow
{
    private PlanetarySystem currentPlanetarySystem = null;
    private PlanetarySystem newPlanetarySystem = null;

    private AstronomicalBody currentGhostBody = null;

    private bool isCreateScreen = true;
    private bool isCreatingAstro = false;

    private string nameInput = "";
    private float radiusInput = 500f;
    private float xVelocityInput = 0f;
    private float yVelocityInput = 0f;
    private float zVelocityInput = 0f;
    private int orbitsAroundInput = 0;

    [MenuItem("Window/Atilla Binal/Planetary System Editor")]
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

            if (isCreatingAstro)
            {
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

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Cancel"))
                {
                    //zerst�rt aktuellen ghost body
                    DestroyImmediate(currentGhostBody.gameObject);
                    currentGhostBody = null;

                    isCreatingAstro = false;
                    Repaint();
                }
                if (GUILayout.Button("Create Body"))
                {
                    CreateAstronomicalBody(nameInput, radiusInput);
                }
                GUILayout.EndHorizontal();

                UpdateGhostStats();
            }
            else
            {
                if (GUILayout.Button("Create New Body"))
                {
                    CreateGhostBody();
                    isCreatingAstro = true;
                    Repaint();
                }
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
        Selection.activeGameObject = universePref;
    }

    private void CreateAstronomicalBody(string name, float radius)
    {
        if (IsSameName(name))
        {
            Debug.LogError("Planet name is already in use. Please choose a different name.");
            return;
        }

        UpdateGhostStats();
        currentGhostBody.gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.white);
        currentGhostBody.gameObject.name = name;

        currentGhostBody = null;

        isCreatingAstro = false;
        Repaint();
    }

    private void CreateGhostBody()
    {
        GameObject bodyObj = Instantiate((GameObject)Resources.Load("PlanetarySystem/prf_Planet"));
        bodyObj.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.red);
        bodyObj.transform.parent = currentPlanetarySystem.transform;
        AstronomicalBody astroBody = bodyObj.GetComponent<AstronomicalBody>();
        currentGhostBody = astroBody;
        astroBody.name = "New Planet";
        bodyObj.name = "New Planet";
        astroBody.SetRadius(radiusInput);
        astroBody.mass = SetMass(radiusInput);

        astroBody.startVelocity = new Vector3(xVelocityInput, yVelocityInput, zVelocityInput);

        astroBody.orbitsAround = currentPlanetarySystem.transform.GetChild(orbitsAroundInput).GetComponent<AstronomicalBody>();

        Repaint();

        Selection.activeGameObject = bodyObj;
    }

    private void UpdateGhostStats()
    {
        //falls zb ghost body gel�scht wird und man landet hier drin ohne dass man ein currentghostbody hat, wir creatingastro window geschlossen 
        if (currentGhostBody == null)
        {
            isCreatingAstro = false;
            Repaint();
            return;
        }

        currentGhostBody.name = nameInput;
        currentGhostBody.SetRadius(radiusInput);
        currentGhostBody.mass = SetMass(radiusInput);
        currentGhostBody.startVelocity = new Vector3(xVelocityInput, yVelocityInput, zVelocityInput);
        currentGhostBody.orbitsAround = currentPlanetarySystem.transform.GetChild(orbitsAroundInput).GetComponent<AstronomicalBody>();
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