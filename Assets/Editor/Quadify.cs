using UnityEngine;
using System.Collections;
using UnityEditor;

public class Quadify : EditorWindow {
    [MenuItem("Window/SpriteQuadifier")]
    static void Init()
    {
        EditorWindow editorWindow = EditorWindow.GetWindow(typeof(Quadify));
        editorWindow.autoRepaintOnSceneChange = true;
        editorWindow.Show();
    }
    public static void ShowWindow()
    {
        //EditorWindow editorWindow = EditorWindow.GetWindow(typeof(Quadify));
    }

    void OnGUI()
    {
        // fetch selection
        GameObject[] selectedObjs = Selection.gameObjects;
        //
        GUILayout.Label("Quadify sprites!", EditorStyles.label);
        if (GUILayout.Button("Do it!"))
        {
            foreach (GameObject obj in selectedObjs)
            {
                QuadifyGameObject(obj);
            }
        }
        GUILayout.Label("Selected GameObjects\n--------------------------------------", EditorStyles.label);
        // Draw list
        foreach (GameObject obj in selectedObjs)
        {
            bool hasSprite = obj.GetComponent<SpriteRenderer>();
            if (hasSprite)
                GUILayout.Label("[ok] "+obj.name, EditorStyles.boldLabel);
            else
                GUILayout.Label(obj.name, EditorStyles.label);
        }
        //myString = EditorGUILayout.TextField("Text Field", myString);
    }

    void QuadifyGameObject(GameObject p_obj)
    {
        SpriteRenderer renderer = p_obj.GetComponent<SpriteRenderer>();
        bool hasSprite = renderer!=null;
        if (hasSprite)
        {
            Sprite spr = ((SpriteRenderer)renderer).sprite;
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Quad);
            plane.name = p_obj.name+"_3D";
            Collider coll = plane.GetComponent<Collider>();
            if (coll) DestroyImmediate(coll);
            // Fix material and texture
            Material mat = CreateMaterial(spr.name);
            mat.mainTexture = spr.texture;
            plane.renderer.sharedMaterial = mat;
            plane.transform.localScale = spr.bounds.size;
            // Fix scaling and offset on texture
            Vector2 sz = new Vector2(spr.rect.size.x / (float)spr.texture.width,
                spr.rect.size.y / (float)spr.texture.height);
            Vector2 off = new Vector2(spr.rect.min.x / (float)spr.texture.width,
                spr.rect.min.y / (float)spr.texture.height);
            plane.renderer.sharedMaterial.mainTextureScale = sz;
            plane.renderer.sharedMaterial.mainTextureOffset = off;
            // Position the quad
            plane.transform.position = p_obj.transform.position;
            // And rotate it
            plane.transform.rotation = p_obj.transform.rotation;
            // same parent
            plane.transform.parent = p_obj.transform.parent;
        }
    }

    Material CreateMaterial(string p_name)
    {
        string pathname = "Assets/Custom Materials/" + p_name + ".mat";
        // Create a simple material asset
        var material = AssetDatabase.LoadAssetAtPath(pathname, typeof(Material));
        if (material==null)
        {
            material = new Material(Shader.Find("Unlit/Transparent Cutout Double Sided"));
            AssetDatabase.CreateAsset(material, "Assets/Custom Materials/" + p_name + ".mat");
        }
        return material as Material;
    }
}
