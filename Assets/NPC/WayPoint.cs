using UnityEngine;
using System.Collections;

public class WayPoint
{
    public WayPoint(Vector3 p_pos)
    {
        m_pos = p_pos;
    }
    public Vector3 m_pos;
    public bool isClose(Vector3 p_other)
    {
        if (Vector3.SqrMagnitude(m_pos-p_other)<0.01f)
        {
            return true;
        }
        return false;
    }
}
