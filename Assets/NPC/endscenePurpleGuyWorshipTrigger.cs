using UnityEngine;
using System.Collections;

public class endscenePurpleGuyWorshipTrigger : MonoBehaviour {
    int paramHash;
    public Animator m_ani;
	// Use this for initialization
	void Start () {
        paramHash = Animator.StringToHash("worship");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void worship_now()
    {
        m_ani.SetTrigger(paramHash);
    }
}
