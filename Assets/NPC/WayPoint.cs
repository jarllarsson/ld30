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
        Vector3 planeDist = new Vector3(m_pos.x - p_other.x, 0.0f, m_pos.z - p_other.z);
        if (Vector3.SqrMagnitude(planeDist)<0.1f)
        {
            return true;
        }
        return false;
    }
}
