using UnityEngine;
using System.Collections;

public class ParticleKill : MonoBehaviour {
    public ParticleSystem m_sys;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!m_sys.isPlaying)
            Destroy(gameObject);
	}
}
