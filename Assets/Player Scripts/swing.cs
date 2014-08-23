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
    public Vector3 m_axis;
    public bool m_instantChange = true;
    private Quaternion m_original;
	// Use this for initialization
	void Start () {
        m_original = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (m_active)
        {
            Quaternion goal = Quaternion.Euler(m_axis * (m_angleDegOffset + m_phasescale * Mathf.Sin(Time.time * m_speed * m_speedMp + m_phaseoffset)));
            if (m_instantChange)
                transform.localRotation = m_original*goal;
            else
            {
                transform.localRotation = m_original * Quaternion.Slerp(transform.localRotation, goal, Time.deltaTime * m_speed);
            }
        }
        else
        {
            if (m_instantChange)
                transform.localRotation = m_original;
            else
                transform.localRotation = Quaternion.Slerp(transform.localRotation, m_original, Time.deltaTime * m_speed);
        }
	}
}
