using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//RedpathN

public class TransformRandomiser : EditorWindow
{

    public GameObject parentObject = null;
    public List<GameObject> children = new List<GameObject>();

    public bool accountForParentScale = true;


    //Rot Params
    string minRotX = "0";
    string maxRotX = "0";
    string minRotY = "0";
    string maxRotY = "360";
    string minRotZ = "0";
    string maxRotZ = "0";

    //Scale Params
    string minScaleX = "0.8";
    string maxScaleX = "1.2";
    string minScaleY = "0.8";
    string maxScaleY = "1.2";
    string minScaleZ = "0.8";
    string maxScaleZ = "1.2";

    private float textWidth = 60;

    [MenuItem("Tools/TransformRandomiser")]
    public static void ShowWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<TransformRandomiser>("TransformRandomiser");
        win.minSize = new Vector2(300, 220);
        win.maxSize = new Vector2(300, 220);
    }

    private void OnGUI()
    {

        GUILayout.BeginHorizontal();
        GUILayout.Label("Parent:", EditorStyles.boldLabel);
        parentObject = (GameObject)EditorGUILayout.ObjectField(parentObject, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("PickParent"))
        {
            PickParent();
        }


        //RotX------------------------------------------
        GUILayout.BeginHorizontal();

        GUILayout.Label("Rot X Min:", EditorStyles.boldLabel);
        minRotX = GUILayout.TextField(minRotX, 8, GUILayout.Width(textWidth));
        GUILayout.Label("Rot X Max:", EditorStyles.boldLabel);
        maxRotX = GUILayout.TextField(maxRotX, 8, GUILayout.Width(textWidth));

        GUILayout.EndHorizontal();

        //RotY-------------------------------------------
        GUILayout.BeginHorizontal();

        GUILayout.Label("Rot Y Min:", EditorStyles.boldLabel);
        minRotY = GUILayout.TextField(minRotY, 8, GUILayout.Width(textWidth));
        GUILayout.Label("Rot Y Max:", EditorStyles.boldLabel);
        maxRotY = GUILayout.TextField(maxRotY, 8, GUILayout.Width(textWidth));

        GUILayout.EndHorizontal();

        //RotZ-------------------------------------------
        GUILayout.BeginHorizontal();

        GUILayout.Label("Rot Z Min:", EditorStyles.boldLabel);
        minRotZ = GUILayout.TextField(minRotZ, 8, GUILayout.Width(textWidth));
        GUILayout.Label("Rot Z Max:", EditorStyles.boldLabel);
        maxRotZ = GUILayout.TextField(maxRotZ, 8, GUILayout.Width(textWidth));

        GUILayout.EndHorizontal();


        if (GUILayout.Button("Randomise Child Rotation"))
        {
            RandomRot();
        }



        //ScaleX------------------------------------------
        GUILayout.BeginHorizontal();

        GUILayout.Label("Scale X Min:", EditorStyles.boldLabel);
        minScaleX = GUILayout.TextField(minScaleX, 8, GUILayout.Width(textWidth));
        GUILayout.Label("Scale X Max:", EditorStyles.boldLabel);
        maxScaleX = GUILayout.TextField(maxScaleX, 8, GUILayout.Width(textWidth));

        GUILayout.EndHorizontal();

        //ScaleY-------------------------------------------
        GUILayout.BeginHorizontal();

        GUILayout.Label("Scale Y Min:", EditorStyles.boldLabel);
        minScaleY = GUILayout.TextField(minScaleY, 8, GUILayout.Width(textWidth));
        GUILayout.Label("Scale Y Max:", EditorStyles.boldLabel);
        maxScaleY = GUILayout.TextField(maxScaleY, 8, GUILayout.Width(textWidth));

        GUILayout.EndHorizontal();

        //ScaleZ-------------------------------------------
        GUILayout.BeginHorizontal();

        GUILayout.Label("Scale Z Min:", EditorStyles.boldLabel);
        minScaleZ = GUILayout.TextField(minScaleZ, 8, GUILayout.Width(textWidth));
        GUILayout.Label("Scale Z Max:", EditorStyles.boldLabel);
        maxScaleZ = GUILayout.TextField(maxScaleZ, 8, GUILayout.Width(textWidth));

        GUILayout.EndHorizontal();


        if (GUILayout.Button("Randomise Child Scale"))
        {
            RandomScale();
        }
        accountForParentScale = GUILayout.Toggle(accountForParentScale, "Account For Parent Scale");

        /*if (GUILayout.Button("Parent the children"))
        {
            Parent();
        }*/
    }


    void PickParent()
    {

        children.Clear();
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
        children.Clear();
        if (parentObject != null)
        {
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                children.Add(childTransform.gameObject);
            }
        }
        
        return;
    }

    float ParseString(string StringText)
    {
        float newFloat;
        
        if (float.TryParse(StringText, out newFloat))
        {
            return newFloat;
        }

        Debug.Log("Parse Failed: Make sure scale values are Int/Float");
        return 0;
    }

    void RandomRot()
    {

        if (children.Count == 0 || children.Count != parentObject.transform.childCount)
        {
            FetchChildren();
        }

        foreach (GameObject child in children)
        {
            float xRot = GetRandom(minRotX, maxRotX);
            float yRot = GetRandom(minRotY, maxRotY);
            float zRot = GetRandom(minRotZ, maxRotZ);

            child.transform.RotateAround(child.transform.position, child.transform.right, xRot);
            child.transform.RotateAround(child.transform.position, child.transform.up, yRot);
            child.transform.RotateAround(child.transform.position, child.transform.forward, zRot);

        }

        return;

    }

    float GetRandom(string a, string b)
    {
        return Random.Range(ParseString(a), ParseString(b));
    }

    void RandomScale()
    {

        if (children.Count == 0 || children.Count != parentObject.transform.childCount)
        {
            FetchChildren();
        }

        foreach (GameObject child in children)
        {

            float xScale = GetRandom(minScaleX, maxScaleX);
            float yScale = GetRandom(minScaleY, maxScaleY);
            float zScale = GetRandom(minScaleZ, maxScaleZ);

            if (accountForParentScale == true)
            {
                xScale = xScale / parentObject.transform.localScale.x;
                yScale = yScale / parentObject.transform.localScale.y;
                zScale = zScale / parentObject.transform.localScale.z;
            }

            child.transform.localScale = new Vector3(xScale, yScale, zScale);
            
        }

        return;

    }
}