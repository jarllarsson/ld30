using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WaypointWalker : MonoBehaviour 
{

    public Stack<WayPoint> m_waypoints = new Stack<WayPoint>();
    private WayPoint m_current;
    private Vector3 m_desiredHeading;
    private float m_withinRangeTimeBeforeAccept = 2.0f;
    private float m_withinRangeTimeBeforeAcceptTick = 0.0f;

    public void clear()
    {
        m_current=null;
        m_waypoints.Clear();
    }

	// Use this for initialization
	void Start () 
    {
        m_withinRangeTimeBeforeAcceptTick = m_withinRangeTimeBeforeAccept;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_desiredHeading = Vector3.zero;
	    if (m_current!=null)
        {
            Vector3 planeMovement = new Vector3(m_current.m_pos.x - transform.position.x, 0.0f, m_current.m_pos.z - transform.position.z);
            m_desiredHeading = Vector3.ClampMagnitude(planeMovement,1.0f);
            if (m_current.isClose(transform.position))
            {
                m_withinRangeTimeBeforeAcceptTick -= Time.deltaTime;
                if (m_withinRangeTimeBeforeAcceptTick<=0.0f)
                {
                    m_current = null;
                    m_withinRangeTimeBeforeAcceptTick = m_withinRangeTimeBeforeAccept;
                }
            }
            else
            {
                m_withinRangeTimeBeforeAcceptTick = m_withinRangeTimeBeforeAccept;
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
