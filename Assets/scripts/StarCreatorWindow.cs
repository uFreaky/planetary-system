using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class StarCreatorWindow : EditorWindow
{
    public float Density
    {
        get { return density; }
        set { density = Mathf.Clamp(value, 0f, 1f); }
    }
    [Range(0f, 1f)] private float density = 0.5f;
    private float size = 0.5f;
    private float sizeRange = 0.4f;
    private float distance = 50f;

    private int maxStarCount = 10000;

    [MenuItem("Window/Atilla Binal/Starfield Creator")]
    public static void ShowWindow()
    {
        GetWindow<StarCreatorWindow>("Starfield Creator");
    }

    private void OnGUI()
    {
        Density = EditorGUILayout.FloatField("Density", Density);
        size = EditorGUILayout.FloatField("Size", size);
        sizeRange = EditorGUILayout.FloatField("Size Range", sizeRange);
        distance = EditorGUILayout.FloatField("Distance", distance);

        if (GUILayout.Button("Create Starfield"))
        {
            CreateStarfield();
        }
    }

    private void CreateStarfield()
    {
        GameObject starfieldObj = Instantiate((GameObject)Resources.Load("PlanetarySystem/Starfield"));

        int starCount = (int)(density * maxStarCount);

        for (int i = 0;  i < starCount; i++)
        {
            GameObject starObj = Instantiate((GameObject)Resources.Load("PlanetarySystem/Star"));
            starObj.transform.Translate(Random.onUnitSphere * distance, Space.World);
            starObj.transform.LookAt(starfieldObj.transform);
            float starSize = size + Random.Range(-(sizeRange / 2), sizeRange / 2);
            starObj.transform.localScale = new Vector3(starSize, starSize, starSize);

            starObj.transform.parent = starfieldObj.transform;
        }
    }
}
#endif