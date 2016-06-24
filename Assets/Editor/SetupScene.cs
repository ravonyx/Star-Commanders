using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
public class SetupScene : EditorWindow
{
    GameObject basePrefab;
    GameObject asteroidPrefab;
    GameObject stationPrefab;
    int taille;
    int nbAsteroid;

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

        EditorGUILayout.LabelField("Taille");
        taille = EditorGUILayout.IntField(taille);
        EditorGUILayout.LabelField("Nb asteroids");
        nbAsteroid = EditorGUILayout.IntField(nbAsteroid);
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("OK"))
            Init(basePrefab, asteroidPrefab, stationPrefab, taille, nbAsteroid);
        if (GUILayout.Button("EraseBase"))
        {
            GameObject[] redBase = GameObject.FindGameObjectsWithTag("RedTeam") as GameObject[];
            ErasePrefab(redBase);
            GameObject[] blueBase = GameObject.FindGameObjectsWithTag("BlueTeam") as GameObject[];
            ErasePrefab(blueBase);
        }
        if (GUILayout.Button("EraseStation"))
        {
            GameObject[] stationPoints = GameObject.FindGameObjectsWithTag("StationPoint") as GameObject[];
            ErasePrefab(stationPoints);
        }
        if (GUILayout.Button("EraseAsteroid"))
        {
            GameObject[] asteroidPoints = GameObject.FindGameObjectsWithTag("AsteroidPoint") as GameObject[];
            ErasePrefab(asteroidPoints);
        }

    }

    static void Init(GameObject basePrefab, GameObject asteroidPrefab, GameObject stationPrefab, int taille, int nbAsteroid)
    {
        if (stationPrefab != null)
        {
            GameObject[] stationPoints = GameObject.FindGameObjectsWithTag("StationPoint") as GameObject[];
            SetupPrefab(stationPrefab, stationPoints);
        }
        if (basePrefab != null && stationPrefab != null)
        {
            GameObject[] redBase = GameObject.FindGameObjectsWithTag("RedTeam") as GameObject[];
            GameObject[] blueBase = GameObject.FindGameObjectsWithTag("BlueTeam") as GameObject[];

            SetupPrefab(basePrefab, redBase);
            SetupPrefab(basePrefab, blueBase);
        }
        if(asteroidPrefab != null)
        {
            GameObject[] asteroidPoints = GameObject.FindGameObjectsWithTag("AsteroidPoint") as GameObject[];
            for (int i = 0; i<nbAsteroid; i++)
            {
                int x, y, z = 0;
                if (i > nbAsteroid / 3)
                {
                    x = Random.Range(-taille / 2, taille / 2);
                    y = Random.Range(-taille, taille);
                    z = Random.Range(-(taille -500), taille - 500);
                }
                else
                {
                    x = Random.Range(-taille, taille);
                    y = Random.Range(-taille, taille);
                    z = Random.Range(-taille, taille);
                }

                GameObject obj = PrefabUtility.InstantiatePrefab(asteroidPrefab) as GameObject;
                obj.transform.parent = asteroidPoints[0].gameObject.transform;
                obj.transform.localPosition = new Vector3(x, y, z);

                int scale = Random.Range(50, 100);
                obj.transform.localScale = new Vector3(scale, scale, scale);
                obj.transform.localRotation = Quaternion.identity;
            }
        }
    }
    static void ErasePrefab(GameObject[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            int nbChild = points[i].gameObject.transform.childCount;
            for (int j = 0; j < nbChild; j++)
            {
                Transform obj = points[i].gameObject.transform.GetChild(0);
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