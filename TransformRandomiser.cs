using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class TransformRandomiser : EditorWindow
{

    public GameObject parentObject;
    public List<GameObject> children;

    [MenuItem("Tools/TransformRandomiser")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TransformRandomiser>("TransformRandomiser");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("PickParent"))
        {
            PickParent();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Parent:", EditorStyles.boldLabel);
        parentObject = (GameObject)EditorGUILayout.ObjectField(parentObject, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Randomise Child Rotation"))
        {
            RandomRot();
        }
        

        /*if (GUILayout.Button("Parent the children"))
        {
            Parent();
        }*/
    }


    void PickParent()
    {
        if (Selection.gameObjects.Length > 0)
        {
            parentObject = Selection.gameObjects[0];
        }
        else
        {
            Debug.Log("Please select a parent object");
        }
    }

    void FetchChildren()
    {
        if (parentObject != null)
        {
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                children.Add(childTransform.gameObject);
            }
        }
    }


    void RandomRot()
    {

        if (children.Count == 0)
        {
            FetchChildren();
        }

        foreach (GameObject child in children)
        {
            float myRandom = Random.Range(0, 360);
            child.transform.Rotate(0, myRandom, 0);
        }
    }
}