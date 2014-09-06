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

	private string m_materialPath="Assets/Custom Materials/";
	private string m_shaderName="Unlit/Transparent Cutout Double Sided";
	private string m_suffix="_3D";
	private Vector2 m_scroll=Vector2.zero;

    void OnGUI()
    {
        // Menu
        GameObject[] selectedObjs = Selection.gameObjects;
        //
        GUILayout.Label("Quadify sprites!", EditorStyles.boldLabel);
		//
		GUILayout.Label("New material (save path)", EditorStyles.miniLabel);
		m_materialPath=GUILayout.TextField(m_materialPath);
		//
		GUILayout.Label("Use shader (name)", EditorStyles.miniLabel);
		m_shaderName=GUILayout.TextField(m_shaderName);
		//
		GUILayout.Label("Quad suffix", EditorStyles.miniLabel);
		m_suffix=GUILayout.TextField(m_suffix);
        if (GUILayout.Button("Quadify!"))
        {
            foreach (GameObject obj in selectedObjs)
            {
                QuadifyGameObject(obj);
            }
        }
        GUILayout.Label("Selected GameObjects\n--------------------------------------", EditorStyles.label);
		m_scroll=GUILayout.BeginScrollView(m_scroll);
        // Draw list
        foreach (GameObject obj in selectedObjs)
        {
            bool hasSprite = obj.GetComponent<SpriteRenderer>();
            if (hasSprite)
			{
                GUILayout.Label("[ok] "+obj.name, EditorStyles.boldLabel);
			}
            else
                GUILayout.Label(obj.name, EditorStyles.label);
        }
		GUILayout.EndScrollView();
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
            plane.name = p_obj.name+m_suffix;
            Collider coll = plane.GetComponent<Collider>();
            if (coll) DestroyImmediate(coll);
            // Fix material and texture
            Material mat = CreateMaterial(spr.name);
            mat.mainTexture = spr.texture;
            plane.renderer.sharedMaterial = mat;
            // Fix scaling and offset on texture
			// Scaling is sprite bounds and its local scale
			plane.transform.localScale = new Vector3(spr.bounds.size.x*p_obj.transform.localScale.x,
			                                         spr.bounds.size.y*p_obj.transform.localScale.y,
			                                         spr.bounds.size.z*p_obj.transform.localScale.z);
			// We need the scaling and offset in the texture
            Vector2 sz = new Vector2(spr.rect.size.x / (float)spr.texture.width,
                spr.rect.size.y / (float)spr.texture.height);
            Vector2 off = new Vector2(spr.rect.min.x / (float)spr.texture.width,
                spr.rect.min.y / (float)spr.texture.height);
			// Set those to our shader
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
        string pathname = m_materialPath + p_name + ".mat";
        // Create a simple material asset
        var material = AssetDatabase.LoadAssetAtPath(pathname, typeof(Material));
        if (material==null)
        {
			material = new Material(Shader.Find(m_shaderName));
            AssetDatabase.CreateAsset(material, m_materialPath + p_name + ".mat");
        }
        return material as Material;
    }
}
