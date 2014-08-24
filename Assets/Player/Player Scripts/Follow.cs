using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    public Transform m_target;
    public Transform m_lookTarget;
    public float m_time = 1.0f;
    private Vector3 m_oldPos;
    private Vector3 m_vel;
    public float m_maxSpeed=1.0f;
	// Use this for initialization
	void Start () 
    {
        m_oldPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector3 target = new Vector3(m_target.position.x, m_target.position.y, m_target.position.z);
        Vector3 smoothMove = Vector3.SmoothDamp(transform.position, target, ref m_vel, m_time,
            m_maxSpeed, Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(m_lookTarget.position - transform.position, Vector3.up);
        //Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, m_rotationOffsetTarget.rotation*transform.rotation, Time.deltaTime);
        transform.position = smoothMove;
        //transform.rotation = smoothRotation;
        m_oldPos = transform.position;
	}
}
