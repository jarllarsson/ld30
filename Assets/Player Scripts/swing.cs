using UnityEngine;
using System.Collections;

public class swing : MonoBehaviour 
{
    public bool m_active=true;
    public float m_phaseoffset;
    public float m_phasescale=1.0f;
    public float m_angleDegOffset;
    public float m_speed=1.0f;
    public float m_speedMp = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (m_active)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, m_angleDegOffset + m_phasescale * Mathf.Sin(Time.time * m_speed * m_speedMp + m_phaseoffset)));
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
	}
}
