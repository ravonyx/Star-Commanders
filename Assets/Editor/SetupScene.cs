using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
public class SetupScene : EditorWindow
{
    GameObject basePrefab;
    GameObject asteroidPrefab;
    GameObject stationPrefab;



    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/SceneSetup")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(SetupScene));
    }

    void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        basePrefab = EditorGUILayout.ObjectField("Prefab des bases", basePrefab, typeof(Object), true) as GameObject;
        asteroidPrefab = EditorGUILayout.ObjectField("Prefab des asteroides", asteroidPrefab, typeof(Object), true) as GameObject;
        stationPrefab = EditorGUILayout.ObjectField("Prefab des stations", stationPrefab, typeof(Object), true) as GameObject;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("OK"))
            Init(basePrefab, asteroidPrefab, stationPrefab);
        if (GUILayout.Button("Erase"))
            Erase();

    }
    static void Erase()
    {
        GameObject[] basePoints = GameObject.FindGameObjectsWithTag("BasePoint") as GameObject[];
        GameObject[] stationPoints = GameObject.FindGameObjectsWithTag("StationPoint") as GameObject[];
        GameObject[] asteroidPoints = GameObject.FindGameObjectsWithTag("AsteroidPoint") as GameObject[];

        ErasePrefab(basePoints);
        ErasePrefab(stationPoints);
        ErasePrefab(asteroidPoints);
    }
    static void Init(GameObject basePrefab, GameObject asteroidPrefab, GameObject stationPrefab)
    {
        if (basePrefab != null && asteroidPrefab != null && stationPrefab != null)
        {
            GameObject[] basePoints = GameObject.FindGameObjectsWithTag("BasePoint") as GameObject[];
            GameObject[] stationPoints = GameObject.FindGameObjectsWithTag("StationPoint") as GameObject[];
            GameObject[] asteroidPoints = GameObject.FindGameObjectsWithTag("AsteroidPoint") as GameObject[];

            SetupPrefab(basePrefab, basePoints);
            SetupPrefab(stationPrefab, stationPoints);
            SetupPrefab(asteroidPrefab, asteroidPoints);
        }
    }

    static void ErasePrefab(GameObject[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            for (int j = 0; j < points[i].gameObject.transform.childCount; j++)
            {
                Transform obj = points[i].gameObject.transform.GetChild(j);
                DestroyImmediate(obj.gameObject);
            }
        }
    }

    static void SetupPrefab(GameObject prefab, GameObject[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            obj.transform.parent = points[i].gameObject.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
}