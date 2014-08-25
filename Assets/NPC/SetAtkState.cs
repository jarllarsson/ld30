using UnityEngine;
using System.Collections;

public class SetAtkState : MonoBehaviour {
    public NPCBrain m_npc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setAtkStateOff()
    {
        m_npc.setAtkState(false);
    }

    public void setAtkStateOn()
    {
        m_npc.setAtkState(true);
    }
}
