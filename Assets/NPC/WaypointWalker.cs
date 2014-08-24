using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WaypointWalker : MonoBehaviour 
{

    public Stack<WayPoint> m_waypoints = new Stack<WayPoint>();
    private WayPoint m_current;
    private Vector3 m_desiredHeading;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (m_current!=null)
        {
            if (m_current.isClose(transform.position))
                m_current = null;
            else
            {
                m_desiredHeading += Vector3.Normalize(m_current.m_pos - transform.position) * Time.deltaTime;
            }
        }
        else if (m_waypoints.Count>0)
        {
            m_current = m_waypoints.Pop();
        }
        debugDraw();
	}

    public Vector3 GetCurrentHeading()
    {
        return m_desiredHeading;
    }

    void debugDraw()
    {
        Vector3 old = transform.position;
        WayPoint[] warray = null;
        if (m_waypoints.Count>0) warray = m_waypoints.ToArray();
        for (int i=-1;i<m_waypoints.Count;i++)
        {
            Vector3 pos = Vector3.zero;
            if (i == -1)
            {
                if (m_current != null)
                    pos = m_current.m_pos;
                else
                    i = 0;
            }
            if (i >= 0 && warray != null)
            {
                pos = warray[i].m_pos;
            }
            else if (m_current == null)
                break;
            Debug.DrawLine(old, pos, Color.yellow);
            old = pos;
        }
    }
}
