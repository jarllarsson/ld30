using UnityEngine;
using System.Collections;

public class FlameBallFollow : MonoBehaviour {
    private GameObject m_player;
    public float m_speed=1.0f;
    public float m_speedNear = 10.0f;
    public float m_slerp=2.0f;
    public Transform m_explode;
    public float startSize = 10.0f;
    private float goalSize;
	// Use this for initialization

	void Start () 
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        goalSize = transform.localScale.x;
        transform.localScale = new Vector3(startSize, startSize, startSize);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 dir = m_player.transform.position + Vector3.up*0.8f - transform.position;
        Quaternion goal = Quaternion.LookRotation(dir);
        if (dir.magnitude>10.0f)
        {
            Vector3 ddir = transform.forward; 
            ddir = new Vector3(ddir.x, ddir.y * 0.5f, ddir.z);
            transform.position += ddir * m_speed * Time.deltaTime;
            transform.rotation = goal;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(goalSize,goalSize,goalSize), 0.5f*Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(goalSize, goalSize, goalSize), 10.0f * Time.deltaTime);
            transform.position += transform.forward * m_speedNear * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, goal, m_slerp*Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider p_coll)
    {
        OnAllCollide(p_coll);
    }

    void OnTriggerStay(Collider p_coll)
    {
        OnAllCollide(p_coll);
    }

    void OnAllCollide(Collider p_coll)
    {
        Debug.Log(p_coll.name);
        Destroy(gameObject);
        if (m_explode)
            Instantiate(m_explode, transform.position, Quaternion.identity);
    }
}
