using UnityEngine;
using System.Collections;

public class unhurter : MonoBehaviour {
    public NPCBrain m_brain;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void feelinFine()
    {
        m_brain.setUnhurt();
    }
}
