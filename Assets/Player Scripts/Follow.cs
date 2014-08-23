using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    public Transform m_target;
    public float m_time = 1.0f;
    public float m_z;
    private Vector3 m_oldPos;
    private Vector3 m_vel;
	// Use this for initialization
	void Start () 
    {
        m_z = transform.position.z;
        m_oldPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector3 target = new Vector3(m_target.position.x, m_target.position.y, m_z);
        Vector3 smooth = Vector3.SmoothDamp(transform.position, target, ref m_vel, m_time);
        transform.position = smooth;
        m_oldPos = transform.position;
	}
}
