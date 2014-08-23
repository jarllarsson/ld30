using UnityEngine;
using System.Collections;

public class wobble : MonoBehaviour 
{
    public bool m_active=true;
    public float m_phaseoffset;
    public float m_phasescale=1.0f;
    public float m_posOffset;
    public float m_speed=1.0f;
    public float m_speedMp = 1.0f;
    Vector3 startPos;
	// Use this for initialization
	void Start () 
    {
        startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (m_active)
        {
            transform.localPosition = new Vector3(startPos.x, m_posOffset + m_phasescale * Mathf.Sin(Time.time * m_speed * m_speedMp + m_phaseoffset), startPos.z);
        }
        else
        {
            transform.localPosition = startPos;
        }
	}
}
