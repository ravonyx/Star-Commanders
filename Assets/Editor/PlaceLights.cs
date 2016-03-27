using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
public class PlaceLights : EditorWindow
{
    Object lightPrefab;
    GameObject lightParent;
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/LightSetup")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(PlaceLights));
    }

    void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        lightParent = EditorGUILayout.ObjectField("Parent Light", lightParent, typeof(Object), true) as GameObject;
        lightPrefab = EditorGUILayout.ObjectField("Prefab Light", lightPrefab, typeof(Object), true);
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("OK"))
            Init(lightPrefab, lightParent);
        if (GUILayout.Button("Erase"))
            Erase(lightParent);

    }
    static void Erase(GameObject lightParent)
    {
        if (lightParent != null)
        {
            for (int i = 0; i < lightParent.transform.childCount; i++)
            {
                Transform room = lightParent.transform.GetChild(i);
                for (int j = 0; j < room.transform.childCount; j++)
                {
                    Transform child = room.transform.GetChild(j);
                    for (int k = 0; k < child.transform.childCount; k++)
                    {
                        Transform light = child.transform.GetChild(k);
                        DestroyImmediate(light.gameObject);
                    }
                }
            }
        }
    }
    static void Init(Object lightPrefab, GameObject lightParent)
    {
        if (lightParent != null)
        {
            for (int i = 0; i < lightParent.transform.childCount; i++)
            {
                Transform room = lightParent.transform.GetChild(i);
                for (int j = 0; j < room.transform.childCount; j++)
                {
                    Transform child = room.transform.GetChild(j);
                    if (child.gameObject.name.StartsWith("LightCube"))
                    {
                        GameObject ligthMain = PrefabUtility.InstantiatePrefab(lightPrefab) as GameObject;
                        ligthMain.transform.parent = child.gameObject.transform;
                        ligthMain.transform.localPosition = Vector3.zero;
                        ligthMain.transform.localScale = Vector3.one;
                        ligthMain.transform.localRotation = Quaternion.identity;
                    }
                }
            }
        }
    }
}